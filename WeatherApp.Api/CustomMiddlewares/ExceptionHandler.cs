using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using WeatherApp.Core.DTOs;

namespace WeatherApp.Api.CustomMiddlewares;

/// <summary>
/// Global exception handler middleware that catches unhandled exceptions 
/// and returns a standardized, structured JSON error response.
/// </summary>
public class ExceptionHandler
{
    private readonly RequestDelegate _delegate;

    public ExceptionHandler(RequestDelegate @delegate)
    {
        _delegate = @delegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _delegate(context);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"An unhandled exception occurred: {exception}");

            string traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = CustomResponse<object>.Failure(
                message: "An unexpected error occurred.",
                statusCode: HttpStatusCode.InternalServerError,
                data: new
                {
                    traceId = traceId
                }
            );

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
