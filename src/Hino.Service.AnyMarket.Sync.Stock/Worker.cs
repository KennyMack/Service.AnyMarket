using Hino.Service.AnyMarket.Application.Products.Interfaces;
using Hino.Service.AnyMarket.Application.Stock.Interfaces;
using Hino.Service.AnyMarket.Domain.Stock.Interfaces.Services;
using Hino.Service.AnyMarket.Sync.Core.Configurations;
using NetSwissTools.System;
using static System.Formats.Asn1.AsnWriter;

namespace Hino.Service.AnyMarket.Sync.Stock
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
            Logs.Logger.LogInformation($"Worker Hino.Service.AnyMarket.Sync.Stock iniciado em: {DateTimeOffset.Now}");
            _logger.LogInformation("Worker Hino.Service.AnyMarket.Sync.Stock iniciado em: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                Logs.Logger.LogInformation($"Worker Hino.Service.AnyMarket.Sync.Stock Iniciando processamento: {DateTimeOffset.Now}");
                _logger.LogInformation("Worker Hino.Service.AnyMarket.Sync.Stock Iniciando processamento: {time}", DateTimeOffset.Now);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    // ******************************
                    // ********** LOCAIS DE ESTOQUE
                    // ******************************
                    Logs.Logger.LogInformation($"Realizando o processamento de locais de estoque: {DateTimeOffset.Now}");
                    _logger.LogInformation("Realizando o processamento de locais de estoque: {time}", DateTimeOffset.Now);

                    var EstoqueManageService = scope.ServiceProvider.GetRequiredService<IMPEstoqueManageAS>();

                    await EstoqueManageService.ManageUploadStockLocalAsync(stoppingToken);

                    Logs.Logger.LogInformation($"Finalizando o processamento de locais de estoque: {DateTimeOffset.Now}");
                    _logger.LogInformation("Finalizando o processamento de locais de estoque: {time}", DateTimeOffset.Now);

                    
                    // ******************************
                    // ********** SALDOS DE ESTOQUE
                    // ******************************
                    Logs.Logger.LogInformation($"Realizando o processamento de saldo de estoque: {DateTimeOffset.Now}");
                    _logger.LogInformation("Realizando o processamento de saldo de estoque: {time}", DateTimeOffset.Now);

                    var SaldoManageService = scope.ServiceProvider.GetRequiredService<IMPEstoqueManageAS>();

                    await SaldoManageService.ManageUploadStockBalanceAsync(stoppingToken);

                    Logs.Logger.LogInformation($"Finalizando o processamento de saldo de estoque: {DateTimeOffset.Now}");
                    _logger.LogInformation("Finalizando o processamento de saldo de estoque: {time}", DateTimeOffset.Now);
                }

                Logs.Logger.LogInformation($"Worker Hino.Service.AnyMarket.Sync.Stock Finalizando processamento: {DateTimeOffset.Now}");
                _logger.LogInformation("Worker Hino.Service.AnyMarket.Sync.Stock Finalizando processamento: {time}", DateTimeOffset.Now);
                await Task.Delay(Settings.Interval, stoppingToken);
            }
        }
    }
}