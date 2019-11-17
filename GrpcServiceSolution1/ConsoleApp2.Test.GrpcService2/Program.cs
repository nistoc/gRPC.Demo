using Grpc.Net.Client;
using GrpcService2;
using System;
using System.Threading.Tasks;
using Grpc.Core;

namespace ConsoleApp2.Test.GrpcService2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WriteLine("Hello World!");

            //await CallOneTime();
            await CallStreamResponse();
        }

        private static async Task CallOneTime()
        {
            Write("Tell me your name:");
            var name = ReadLine();
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(new HelloRequest {Name = name});
            WriteLine(response.Message);
            ReadLine();
        }

        private static async Task CallStreamResponse()
        {

            Write("Tell me your name:");
            var name = ReadLine();
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var call = client.SayHelloStreamig(new HelloRequest { Name = name });
            WriteLine("Read responses by While");
            var t = 5;
            while (await call.ResponseStream.MoveNext() && t > 0)
            {
                
                WriteLine(call.ResponseStream.Current.Message);
                t--;
            }

            WriteLine("Read responses by grpc.core.ReadAllAsync()");
            await foreach (var message in call.ResponseStream.ReadAllAsync())
            {
                WriteLine(message.Message);
            }

            WriteLine("Hit any key to exit");
            ReadLine();
        }

        private static string ReadLine() => Console.ReadLine();
        private static void Write(string text) => Console.Write(text);
        private static void WriteLine(string text) => Console.WriteLine(text);
    }
}
