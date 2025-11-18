namespace WalletBroAPI.Common.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object>(
                IsSuccess: false,
                Message: "An unexpected error occurred",
                Data: null,
                Errors: null
            );

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}