using Microsoft.AspNetCore.Hosting;

namespace NetCoreOnDocker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:80/")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}