using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WeatherApp.Api;
using WeatherApp.Api.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InjectAddEndpointsApiExplorer()
    .InjectAddSwaggerGen()
    .InjectControllers()
    .InjectFluentValidation()
    .InjectHealthCheck()
    .InjectHttpContextAccessor()
    .InjectExternalServices()
    .InjectServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
});

app.Run();

