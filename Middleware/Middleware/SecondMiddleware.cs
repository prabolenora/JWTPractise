namespace JWTAuthorization.Middleware
{
    public class SecondMiddleware : IMiddleware
    {
        async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var val = "this just for testing purpose";
            await next(context);
        }
    }
}
