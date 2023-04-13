using PizzApp.Web.Infrastructure.Middlewares;

namespace PizzApp.API.Infrastructure.Extensions;

public static class CultureMiddlewareExtension
{
    public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder app)
    {
        return app.UseMiddleware<Culturemiddleware>();
    }
}
