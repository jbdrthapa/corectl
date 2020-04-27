using System;
using System.Threading.Tasks;

namespace Core.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Print CPUInfo
            await PrintCPUInfo();

            Console.WriteLine("Press any key to exit...");

            Console.Read();
        }

        private static async Task PrintCPUInfo()
        {
            var cpuInfoClient = ClientsFactory.CpuInfoClient;

            var reply = await cpuInfoClient.GetCpuInfoAsync(new Protos.CpuInfoRequest());

            Console.WriteLine($"Cpu Model Name: {reply.ModelName}");
            Console.WriteLine($"Cpu Model: {reply.Model}");
            Console.WriteLine($"Cpu Cores: {reply.Cores}");
            Console.WriteLine($"Cpu Cache: {reply.Cache}");
            Console.WriteLine($"Cpu Family: {reply.Family}");
            Console.WriteLine($"Cpu Flags: {reply.Flags}");
            Console.WriteLine($"Cpu MHz: {reply.MHz}");
            Console.WriteLine($"Cpu Microcode: {reply.Microcode}");
            Console.WriteLine($"Cpu Stepping: {reply.Stepping}");
            Console.WriteLine($"Cpu Vendor: {reply.Vendor}");
            Console.WriteLine($"Cpu VMX Flags: {reply.VMXFlags}");
        }
    }
}
