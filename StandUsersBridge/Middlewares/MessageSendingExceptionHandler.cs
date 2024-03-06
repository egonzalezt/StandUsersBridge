namespace StandUsersBridge.Middlewares;

using System.Net;
using System.Text.Json;
public class MessageSendingExceptionHandler(RequestDelegate next, ILogger<MessageSendingExceptionHandler> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<MessageSendingExceptionHandler> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An error occurred while processing the request.");

        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Internal server error";

        if (exception is RabbitMQ.Client.Exceptions.BrokerUnreachableException ||
            exception is RabbitMQ.Client.Exceptions.PossibleAuthenticationFailureException)
        {
            statusCode = HttpStatusCode.InternalServerError;
            message = "Internal server error";
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        var result = JsonSerializer.Serialize(new { message });
        await context.Response.WriteAsync(result);
    }
}
