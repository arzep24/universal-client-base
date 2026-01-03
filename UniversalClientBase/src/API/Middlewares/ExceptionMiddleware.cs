using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace UniversalClientBase.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Continúa el flujo normal de la petición
        }
        catch( Exception ex)
        {
            _logger.LogError(ex, ex.Message); // Registra el error
            await HandleExceptionAsync(context, ex); //Procesa la respuesta JSON
        }
        
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Estructura siguiendo el estandár ProblemDetails
        var response = new ProblemDetails
        {
            Status = context.Response.StatusCode,
            Title = "Error Interno del Servidor",
            Detail = _env.IsDevelopment() ? exception.Message : "Ocurrió un error inesperado. Intente más tarde.",
            Instance = context.Request.Path
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}