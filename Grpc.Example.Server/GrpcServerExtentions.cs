using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Grpc.Example.Server
{
    public static class GrpcServerExtentions
    {
        public static IWebHostBuilder UseGrpc<T>(this IWebHostBuilder hostBuilder)
            where T : Greeter.GreeterBase
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IServer, GrpcServer>(provider =>
                {
                    var serverOptions = provider.GetService<IOptions<ServiceOptions>>().Value;
                    var contract = provider.GetService<T>();
                    var serviceDifinition = Greeter.BindService(contract);
                    return new GrpcServer(serverOptions.Host, serverOptions.Port, serviceDifinition);
                });
            });
        }

        public static IWebHost CreateScope(this IWebHost webhost)
        {
            //TODO: add creating scope into interceptor
            return webhost;
        }

        public static IWebHost RunGrpcServer(this IWebHost webhost)
        {
            var scope = webhost.Services.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<Greeter.GreeterBase>();
            var serviceOptions = scope.ServiceProvider.GetRequiredService<IOptions<ServiceOptions>>().Value;
            var serviceDifinition = Greeter.BindService(service);

            using (var server = new GrpcServer(serviceOptions.Host, serviceOptions.Port, serviceDifinition))
            {
                server.Start();
            }

            return webhost;
        }
    }
}