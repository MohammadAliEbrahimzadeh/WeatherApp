using System.Net;

namespace WeatherApp.Core.DTOs;

/// <summary>
/// A standardized, generic response wrapper for returning data and explicit status 
/// between application layers and to external consumers.
/// </summary>
/// <typeparam name="T">The type of the data payload.</typeparam>
public class CustomResponse<T>
{
    // Use 'init' instead of 'set' to make properties immutable after creation.
    public T? Data { get; init; }
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
    public HttpStatusCode StatusCode { get; init; }
    public int TotalCount { get; init; }

    /// <summary>
    /// Required for JSON Deserialization (e.g., in HttpClient calls or Web API responses).
    /// </summary>
    public CustomResponse() { }

    /// <summary>
    /// Private constructor used internally by factory methods.
    /// </summary>
    private CustomResponse(T? data, bool isSuccess, string? message, HttpStatusCode statusCode, int totalCount)
    {
        Data = data;
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode;
        TotalCount = totalCount;
    }

    // --- Factory Methods for Success ---

    public static CustomResponse<T> Success(T? data, HttpStatusCode statusCode = HttpStatusCode.OK, int totalCount = 0)
    {
        return new CustomResponse<T>(data, true, "Operation successful", statusCode, totalCount);
    }

    // --- Factory Methods for Failure ---

    public static CustomResponse<T> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, T? data = default)
    {
        return new CustomResponse<T>(data, false, message, statusCode, 0);
    }

    /// <summary>
    /// Helper for failure cases where the data type is irrelevant (e.g., City Not Found).
    /// </summary>
    public static CustomResponse<T> Failure(string message, HttpStatusCode statusCode)
    {
        return Failure(message, statusCode, default);
    }
}
