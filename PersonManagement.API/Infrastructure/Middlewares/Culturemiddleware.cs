using System.Globalization;

namespace PizzApp.Web.Infrastructure.Middlewares
{
    public class Culturemiddleware
    {
        private readonly RequestDelegate _next;

        public Culturemiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var cultureName = "ka-GE";

            var queryCulture = context.Request.Headers["Accept-Language"].ToString();

            if (!string.IsNullOrWhiteSpace(queryCulture))
                cultureName = queryCulture.Split(',')[0];

            var culture = new CultureInfo(cultureName);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(context);
        }
    }
}
