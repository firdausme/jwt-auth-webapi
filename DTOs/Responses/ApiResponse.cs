namespace JwtAuthWebApi.DTOs.Responses;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, string[]>? ValidateError { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> Success(string message) => new() { IsSuccess = true, Message = message };

    public static ApiResponse<T> Success(T? data, string message = "Operation Succeeded") =>
        new() { IsSuccess = true, Message = message, Data = data };

    public static ApiResponse<T> Failure(string message) =>
        new() { IsSuccess = false, ValidateError = null, Message = message };

    public static ApiResponse<T> Failure(Dictionary<string, string[]> validateError, string message) =>
        new() { IsSuccess = false, ValidateError = validateError, Message = message };
}