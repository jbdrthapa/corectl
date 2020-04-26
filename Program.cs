using System;
using System.Net.Http;
using System.Threading.Tasks;
using coreinfo;
using Grpc.Net.Client;

namespace corectl
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest{ Name = "GreeterClient" });
            Console.WriteLine("Greeter: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
