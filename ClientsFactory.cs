using System.Net.Http;
using Grpc.Core;
using Grpc.Net.Client;
using Core.Protos;

namespace Core.Client
{
    public static class ClientsFactory
    {
        private static ChannelBase _channel;
        private static CpuInfo.CpuInfoClient _cpuInfoClient;

        static ClientsFactory()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var httpClient = new HttpClient(httpClientHandler);

            _channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
        }

        public static CpuInfo.CpuInfoClient CpuInfoClient
        {
            get
            {
                if (_cpuInfoClient == null)
                {
                    _cpuInfoClient = new CpuInfo.CpuInfoClient(_channel);
                }

                return _cpuInfoClient;
            }
        }
    }
}