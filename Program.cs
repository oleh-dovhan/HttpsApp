using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using HttpsApp.Extensions;

namespace HttpsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options => options.ConfigureEndpoints())
                .Build();
    }
}
