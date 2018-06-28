using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Example.Server.Repositories;

namespace Grpc.Example.Server
{
    public class GreeterServiceImpl : Greeter.GreeterBase
    {
        private readonly ISomeRepository _someRepository;

        public GreeterServiceImpl(ISomeRepository someRepository)
        {
            _someRepository = someRepository;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply() {Message = _someRepository.SayHello() + request.Name});
        }

        public override Task<HelloReply> SayHelloAgain(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply() {Message = _someRepository.SayHelloAgain() + request.Name});
        }
    }
}