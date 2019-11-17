using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService2
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayHelloStreamig(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Hello {request.Name} {i}"
                });
            }

            await responseStream.WriteAsync(new HelloReply
            {
                Message = $"{request.Name}, you should go to server console. Alt+Tab?"
            });

            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine($"Please response to Client (more {i} lines(s)): ");
                var text = Console.ReadLine();
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Server response is: {text}"
                });
            }
        }
    }
}
