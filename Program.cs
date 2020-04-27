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
            var cpuInfoClient = ClientsFactory.CpuInfoClient;

            var reply = await cpuInfoClient.GetCpuInfoAsync(new Protos.CpuInfoRequest());

            Console.WriteLine("Cpu Model: " + reply.Model);
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
