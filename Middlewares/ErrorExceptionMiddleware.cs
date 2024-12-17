using JwtAuthWebApi.DTOs.Responses;
using JwtAuthWebApi.Exceptions;

namespace JwtAuthWebApi.Middlewares;

public class ErrorExceptionMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context); // Proceed to the next middleware in the pipeline
        }
        catch (FluentValidation.ValidationException ex)
        {
            await WriteApiResponseAsync(
                context,
                ApiResponse<object>.Failure(
                    ex.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(to => to.Key, to => to.Select(e => e.ErrorMessage).ToArray()),
                    "Validation errors occurred."
                ),
                StatusCodes.Status422UnprocessableEntity
            );
        }
        catch (ApiException ex)
        {
            if(context.Response.HasStarted) return;
            
            await WriteApiResponseAsync(
                context,
                new ApiResponse<object>() { Message = ex.Message },
                ex.StatusCode
            );
        }
        catch (Exception ex)
        {
            if(context.Response.HasStarted) return;
            
            await WriteApiResponseAsync(
                context,
                new ApiResponse<object>() { Message = ex.Message },
                StatusCodes.Status500InternalServerError
            );

            Console.WriteLine(ex.StackTrace);
        }
    }

    private static Task WriteApiResponseAsync(
        HttpContext context,
        ApiResponse<object> response,
        int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsJsonAsync(response);
    }
}