namespace Web.Middleware
{
    using Castle.MicroKernel.Lifestyle;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class WindsorScope
    {

        private readonly RequestDelegate _next;

        public WindsorScope(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (HostingConfig.GetContainer().BeginScope())
            {
                await _next.Invoke(context);
            }
        }
    }
}
