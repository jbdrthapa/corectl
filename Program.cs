using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace Core.Client
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
            
            var client = new Core.Protos.CpuInfo.CpuInfoClient(channel);

            var reply = await client.GetCpuInfoAsync(new Protos.CpuInfoRequest());
            Console.WriteLine("Cpu Model: " + reply.Model);
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
