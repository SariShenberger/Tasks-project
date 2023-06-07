using tasks._0.Interfaces;

namespace tasks._0.Middlewares;

public class LogMiddleware
{
    private readonly ILogService logger;
    private readonly RequestDelegate next;
     public LogMiddleware(RequestDelegate next, ILogService logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        logger.Log(LogLevel.Debug, $"start {ctx.Request.Path}");
        await next(ctx);
        logger.Log(LogLevel.Debug, $"end {ctx.Request.Path}");
    }
}