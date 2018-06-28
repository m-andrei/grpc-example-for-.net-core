using System;
using Grpc.Core;

namespace Grpc.Example.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Channel channel = new Channel("localhost:8081", ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);
            
            try
            {
                var reply = client.SayHello(new HelloRequest() {Name= "test"});
                Console.WriteLine("Count: " + reply.Message);
                
                var reply2 = client.SayHelloAgain(new HelloRequest() {Name= "test"});
                Console.WriteLine("Count: " + reply2.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
                throw;
            }

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}