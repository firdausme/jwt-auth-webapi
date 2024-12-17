using System.Text;
using System.Text.RegularExpressions;
using Serilog;

namespace JwtAuthWebApi.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next, IConfiguration config)
{
    public async Task Invoke(HttpContext context)
    {
        // Log Request Query Params and Headers
        Log.Information("Request: {Method} {Path} {QueryString} {Headers}",
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString,
            context.Request.Headers);

        // Read the body (to log it)
        var bodyAsText = await ReadRequestBodyAsync(context.Request);

        // Log the Request Body (You can adjust this to log specific fields, avoid logging sensitive data)
        if (!string.IsNullOrWhiteSpace(bodyAsText))
        {
            bodyAsText = MaskSensitiveData(bodyAsText, config);
            Log.Information("Request Body: {RequestBody}", bodyAsText);
        }

        // Ensure the request body is still available for the next middleware
        // context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(bodyAsText));

        // Proceed to the next middleware in the pipeline
        await next(context);
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        // Enable buffering to allow reading the body multiple times
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();

        // Reset the stream position so other middlewares/controllers can read it
        request.Body.Position = 0;

        return body;
    }

    private static string MaskSensitiveData(string body, IConfiguration config)
    {
        var attributes = config.GetSection("MaskSensitiveData:Attributes").Get<List<string>>();
        if (attributes == null) return body;

        return attributes.Aggregate(body, (currentBody, attribute) =>
            Regex.Replace(currentBody,
                $"\"{attribute}\"\\s*:\\s*\"[^\"]*\"",
                $"\"{attribute}\":\"******\"",
                RegexOptions.IgnoreCase));
    }
}