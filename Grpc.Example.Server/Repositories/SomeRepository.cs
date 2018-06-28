namespace Grpc.Example.Server.Repositories
{
    public class SomeRepository : ISomeRepository
    {
        public string SayHello()
        {
            return "Hello: ";
        }

        public string SayHelloAgain()
        {
            return "Hello Again: ";
        }
    }
}