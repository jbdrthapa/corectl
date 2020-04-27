using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Core.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Print CPUInfo
            await GetCPUInfoAsync();

            await Task.Run(() =>
            {
                _ = StreamCPUInfoAsync();

            });

            Console.WriteLine("Press any key to exit...");

            Console.Read();
        }

        private static async Task StreamCPUInfoAsync()
        {
            var cpuInfoClient = ClientsFactory.CpuInfoClient;

            var cancellationTokenSource = new CancellationTokenSource();

            using var streamingCpuInfo = cpuInfoClient.GetCpuInfoStream(new Empty(), null, null, cancellationTokenSource.Token);

            try
            {
                await foreach (var cpuInfoData in streamingCpuInfo.ResponseStream.ReadAllAsync(cancellationToken: cancellationTokenSource.Token))
                {
                    await PrintCPUInfoAsync(cpuInfoData);
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                await Console.Out.WriteLineAsync("Cpu Info stream cancelled.");
            }
        }

        private static async Task GetCPUInfoAsync()
        {
            var cpuInfoClient = ClientsFactory.CpuInfoClient;

            var cpuInfoData = await cpuInfoClient.GetCpuInfoAsync(new Empty());

            await PrintCPUInfoAsync(cpuInfoData);
        }

        private static async Task PrintCPUInfoAsync(CpuInfoData cpuInfoData)
        {
            await Console.Out.WriteLineAsync($"Timestamp: {cpuInfoData.DateTimeStamp}");
            await Console.Out.WriteLineAsync($"Cpu Model Name: {cpuInfoData.ModelName}");
            await Console.Out.WriteLineAsync($"Cpu Model: {cpuInfoData.Model}");
            await Console.Out.WriteLineAsync($"Cpu Cores: {cpuInfoData.Cores}");
            await Console.Out.WriteLineAsync($"Cpu Cache: {cpuInfoData.Cache}");
            await Console.Out.WriteLineAsync($"Cpu Family: {cpuInfoData.Family}");
            await Console.Out.WriteLineAsync($"Cpu Flags: {cpuInfoData.Flags}");
            await Console.Out.WriteLineAsync($"Cpu MHz: {cpuInfoData.MHz}");
            await Console.Out.WriteLineAsync($"Cpu Microcode: {cpuInfoData.Microcode}");
            await Console.Out.WriteLineAsync($"Cpu Stepping: {cpuInfoData.Stepping}");
            await Console.Out.WriteLineAsync($"Cpu Vendor: {cpuInfoData.Vendor}");
            await Console.Out.WriteLineAsync($"Cpu VMX Flags: {cpuInfoData.VMXFlags}");

        }
    }
}
