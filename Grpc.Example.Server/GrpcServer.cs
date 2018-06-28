using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;

namespace Grpc.Example.Server
{
    public class GrpcServer : IServer
    {
        private readonly Core.Server _server;
        private int _stopped;

        public GrpcServer(string host, int port, ServerServiceDefinition serviceDifinition)
        {
            _server = new Core.Server
            {
                Services =
                {
                    serviceDifinition
                },
                Ports = {new ServerPort(host, port, ServerCredentials.Insecure)}
            };
            Features = new FeatureCollection();
            Features.Set<IServerAddressesFeature>(new ServerAddressesFeature() {Addresses = {$"http://{host}:{port}"}});
        }

        public void Dispose()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            this.StopAsync(cancellationTokenSource.Token).GetAwaiter().GetResult();
            Console.WriteLine("Server has stopped");
        }

        public async Task StartAsync<TContext>(IHttpApplication<TContext> application,
            CancellationToken cancellationToken)
        {
            _server.Start();
        }

        public void Start()
        {
            _server.Start();

            Console.WriteLine("Grpc server listening");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (Interlocked.Exchange(ref this._stopped, 1) == 1)
                return;
            await _server.ShutdownAsync();
        }

        public IFeatureCollection Features { get; }
    }
}