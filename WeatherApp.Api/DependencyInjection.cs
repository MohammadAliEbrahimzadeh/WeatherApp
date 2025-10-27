using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Net;
using WeatherApp.Core.Contracts;
using WeatherApp.Core.DTOs;
using WeatherApp.Core.Implementations;

namespace WeatherApp.Api;

internal static class DependencyInjection
{
    internal static IServiceCollection InjectHttpContextAccessor(this IServiceCollection services) =>
       services.AddHttpContextAccessor();

    internal static IServiceCollection InjectAddEndpointsApiExplorer(this IServiceCollection services) =>
       services.AddEndpointsApiExplorer();

    internal static IServiceCollection InjectControllers(this IServiceCollection services) =>
        services.AddControllers()
                .Services;


    internal static IServiceCollection InjectAddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WeatherApp.Api",
                Version = "v1",
                Description = "API documentation for retrieving weather and air quality data."
            });

            c.CustomSchemaIds(type => type.FullName);

            // --- Security Definition ---
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        });

        return services;
    }



    internal static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IWeatherService, WeatherService>();
        return services;
    }

    internal static IServiceCollection InjectExternalServices(this IServiceCollection services) =>
        services.AddScoped<WeatherDataProvider>();


    internal static IServiceCollection InjectFluentValidation(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblyContaining<CityQueryDto>()
        .AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

    internal static IServiceCollection InjectHealthCheck(this IServiceCollection services)
    {
        return services.AddHealthChecks()
           .AddCheck<WeatherApiHealthCheck>(
               "WeatherAPI",
               failureStatus: HealthStatus.Unhealthy,
               tags: new[] { "external" }).Services;
    }
}
