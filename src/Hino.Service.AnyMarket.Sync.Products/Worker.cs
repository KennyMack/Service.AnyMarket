using Hino.Service.AnyMarket.Application.Products.Interfaces;
using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.DataBase.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services;
using Hino.Service.AnyMarket.Sync.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using NetSwissTools.System;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Sync.Products
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
            Logs.Logger.LogInformation($"Worker Hino.Service.AnyMarket.Sync.Products iniciado em: {DateTimeOffset.Now}");
            _logger.LogInformation("Worker Hino.Service.AnyMarket.Sync.Products iniciado em: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                Logs.Logger.LogInformation($"Worker Hino.Service.AnyMarket.Sync.Products Iniciando processamento: {DateTimeOffset.Now}");
                _logger.LogInformation("Worker Hino.Service.AnyMarket.Sync.Products Iniciando processamento: {time}", DateTimeOffset.Now);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    
                    // ******************************
                    // ********** MARCAS
                    // ******************************
                    Logs.Logger.LogInformation($"Realizando o processamento de marcas: {DateTimeOffset.Now}");
                    _logger.LogInformation("Realizando o processamento de marcas: {time}", DateTimeOffset.Now);

                    var MarcasManageService = scope.ServiceProvider.GetRequiredService<IMPMarcasManageAS>();

                    await MarcasManageService.ManageUploadAsync(stoppingToken);

                    Logs.Logger.LogInformation($"Finalizando o processamento de marcas: {DateTimeOffset.Now}");
                    _logger.LogInformation("Finalizando o processamento de marcas: {time}", DateTimeOffset.Now);

                    
                    // ******************************
                    // ********** CATEGORIAS
                    // ******************************
                    Logs.Logger.LogInformation($"Realizando o processamento de categorias: {DateTimeOffset.Now}");
                    _logger.LogInformation("Realizando o processamento de categorias: {time}", DateTimeOffset.Now);

                    var CategoriasManageService = scope.ServiceProvider.GetRequiredService<IMPCategoriasManageAS>();

                    await CategoriasManageService.ManageUploadAsync(stoppingToken);

                    Logs.Logger.LogInformation($"Finalizando o processamento de categorias: {DateTimeOffset.Now}");
                    _logger.LogInformation("Finalizando o processamento de categorias: {time}", DateTimeOffset.Now);
                    
                    // ******************************
                    // ********** TIPOS VARIAÇÃO
                    // ******************************
                    Logs.Logger.LogInformation($"Realizando o processamento dos tipos de variação: {DateTimeOffset.Now}");
                    _logger.LogInformation("Realizando o processamento dos tipos de variação: {time}", DateTimeOffset.Now);

                    var TiposVariacaoService = scope.ServiceProvider.GetRequiredService<IMPTiposVariacaoAS>();

                    await TiposVariacaoService.ManageUploadAsync(stoppingToken);

                    Logs.Logger.LogInformation($"Finalizando o processamento dos tipos de variação: {DateTimeOffset.Now}");
                    _logger.LogInformation("Finalizando o processamento dos tipos de variação: {time}", DateTimeOffset.Now);
                    
                    
                    // ******************************
                    // ********** PRODUTOS
                    // ******************************
                    Logs.Logger.LogInformation($"Realizando o processamento de produtos: {DateTimeOffset.Now}");
                    _logger.LogInformation("Realizando o processamento de produtos: {time}", DateTimeOffset.Now);
                    
                    var ProdutosManageAS = scope.ServiceProvider.GetRequiredService<IMPProdutosManageAS>();
                    
                    await ProdutosManageAS.ManageUploadAsync(stoppingToken);
                    
                    Logs.Logger.LogInformation($"Finalizando o processamento de produtos: {DateTimeOffset.Now}");
                    _logger.LogInformation("Finalizando o processamento de produtos: {time}", DateTimeOffset.Now);
                    
                    

                }

                Logs.Logger.LogInformation($"Worker Hino.Service.AnyMarket.Sync.Products Finalizando processamento: {DateTimeOffset.Now}");
                _logger.LogInformation("Worker Hino.Service.AnyMarket.Sync.Products Finalizando processamento: {time}", DateTimeOffset.Now);
                await Task.Delay(Settings.Interval, stoppingToken);
            }
        }
    }
}