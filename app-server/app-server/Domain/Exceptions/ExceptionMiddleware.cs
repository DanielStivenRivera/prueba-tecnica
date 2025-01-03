using System.Text.Json;

namespace app_server.Domain.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            if (ex is UserAlreadyRegisteredException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = ex.Message }));
            }
            else if (ex is UnauthorizedAccessException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = ex.Message }));
            }
            else if (ex is InvalidOperationException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = ex.Message }));
            } else if (ex is ReservationException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = ex.Message }));
            }
            else
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = "Internal Server Error" }));
            }
        }

    }
}