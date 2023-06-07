using tasks._0.Middlewares;

namespace tasks._0.Utilities;

public static class MiddlewareExtensions
{
   
    public static IApplicationBuilder UseLogMiddleware(
        this IApplicationBuilder app
    )
    {
        return app.UseMiddleware<LogMiddleware>();
       
    }

    

    
}

