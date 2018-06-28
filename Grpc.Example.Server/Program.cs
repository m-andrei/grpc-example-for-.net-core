using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Grpc.Example.Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .CreateScope()
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", false, true);
                })
                .UseStartup<Startup>()
                .UseGrpc<GreeterServiceImpl>()
                .Build();
    }
}