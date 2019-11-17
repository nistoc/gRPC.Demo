using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcService2;

namespace ConsoleApp2.Test.GrpcService2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WriteLine("Hello World!");

            Write("Tell me your name:");
            var name = ReadLine();
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(new HelloRequest{Name = name});
            WriteLine(response.Message);
            ReadLine();
        }

        private static string ReadLine() => Console.ReadLine();
        private static void Write(string text) => Console.Write(text);
        private static void WriteLine(string text) => Console.WriteLine(text);
    }
}
