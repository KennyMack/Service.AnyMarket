using Hino.Service.AnyMarket.Application.Orders.Interfaces;
using Hino.Service.AnyMarket.Application.Stock.Interfaces;
using Hino.Service.AnyMarket.Sync.Core.Configurations;
using NetSwissTools.System;

namespace Hino.Service.AnyMarket.Sync.Orders
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
        {
            Settings.Interval = ConvertEx.ToInt32(configuration["Interval"]) ?? 0;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logs.Logger.LogInformation($"Worker Hino.Service.AnyMarket.Sync.Orders iniciado em: {DateTimeOffset.Now}");
            _logger.LogInformation("Worker Hino.Service.AnyMarket.Sync.Orders iniciado em: {time}", DateTimeOffset.Now);



            while (!stoppingToken.IsCancellationRequested)
            {
                Logs.Logger.LogInformation($"Worker Hino.Service.AnyMarket.Sync.Orders Iniciando processamento: {DateTimeOffset.Now}");
                _logger.LogInformation("Worker Hino.Service.AnyMarket.Sync.Orders Iniciando processamento: {time}", DateTimeOffset.Now);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    // ******************************
                    // ********** FEED ORDERS
                    // ******************************
                    Logs.Logger.LogInformation($"Realizando o processamento da fila de pedidos: {DateTimeOffset.Now}");
                    _logger.LogInformation("Realizando o processamento da fila de pedidos: {time}", DateTimeOffset.Now);

                    var OrderReceived = scope.ServiceProvider.GetRequiredService<IOrderReceivedAS>();

                    await OrderReceived.ReceiveNewOrdersAsync(stoppingToken);

                    Logs.Logger.LogInformation($"Finalizando o processamento da fila de pedidos: {DateTimeOffset.Now}");
                    _logger.LogInformation("Finalizando o processamento da fila de pedidos: {time}", DateTimeOffset.Now);
                }

                Logs.Logger.LogInformation($"Worker Hino.Service.AnyMarket.Sync.Orders Finalizando processamento: {DateTimeOffset.Now}");
                _logger.LogInformation("Worker Hino.Service.AnyMarket.Sync.Orders Finalizando processamento: {time}", DateTimeOffset.Now);
                await Task.Delay(Settings.Interval, stoppingToken);
            }
        }
    }
}