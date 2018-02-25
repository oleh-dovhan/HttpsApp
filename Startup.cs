using HttpsApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpsApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            int? httpsPort = null;
            var httpsSection = Configuration.GetSection("HttpServer:Endpoints:Https");
            if (httpsSection.Exists())
            {
                var httpsEndpoint = new EndpointConfiguration();
                httpsSection.Bind(httpsEndpoint);
                httpsPort = httpsEndpoint.Port;
            }
            var statusCode = env.IsDevelopment() ? StatusCodes.Status302Found : StatusCodes.Status301MovedPermanently;
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(statusCode, httpsPort));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
