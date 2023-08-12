using Hino.Service.AnyMarket.Application.Orders.Interfaces;
using Hino.Service.AnyMarket.Application.Orders.Services;
using Hino.Service.AnyMarket.Application.Products.Interfaces;
using Hino.Service.AnyMarket.Application.Products.Services;
using Hino.Service.AnyMarket.Application.Stock.Interfaces;
using Hino.Service.AnyMarket.Application.Stock.Services;
using Hino.Service.AnyMarket.DataBase.Repositories.CRM;
using Hino.Service.AnyMarket.DataBase.Repositories.Fiscal;
using Hino.Service.AnyMarket.DataBase.Repositories.General;
using Hino.Service.AnyMarket.DataBase.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Services;
using Hino.Service.AnyMarket.Domain.Orders.Services;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Marcas;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Products;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Variacoes;
using Hino.Service.AnyMarket.Domain.Products.Services.Categorias;
using Hino.Service.AnyMarket.Domain.Products.Services.Marcas;
using Hino.Service.AnyMarket.Domain.Products.Services.Products;
using Hino.Service.AnyMarket.Domain.Products.Services.Variacoes;
using Hino.Service.AnyMarket.Domain.Stock.Interfaces.Repositories;
using Hino.Service.AnyMarket.Domain.Stock.Interfaces.Services;
using Hino.Service.AnyMarket.Domain.Stock.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hino.Service.AnyMarket.IoC.Services
{
    internal static class BindingMarketplace
    {
        public static void AddMarketplaceInstances(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IMPMarcasRepository, MPMarcasRepository>();
            services.AddTransient<IMPProdAtributosRepository, MPProdAtributosRepository>();
            services.AddTransient<IMPProdEstoqueRepository, MPProdEstoqueRepository>();
            services.AddTransient<IMPProdImagensRepository, MPProdImagensRepository>();
            services.AddTransient<IMPProdPrecoRepository, MPProdPrecoRepository>();
            services.AddTransient<IMPProdutosRepository, MPProdutosRepository>();
            services.AddTransient<IMPCategoriasRepository, MPCategoriasRepository>();
            services.AddTransient<IMPTiposVariacaoRepository, MPTiposVariacaoRepository>();
            services.AddTransient<IMPTiposVarValoresRepository, MPTiposVarValoresRepository>();
            services.AddTransient<IMPEstoqueRepository, MPEstoqueRepository>();
            services.AddTransient<IFSVWProdSaldoEstGrupoDisponivelRepository, FSVWProdSaldoEstGrupoDisponivelRepository>();

            services.AddTransient<IGEQueueItemRepository, GEQueueItemRepository>();
            services.AddTransient<ICROrdersRepository, CROrdersRepository>();
            services.AddTransient<ICROrderItemsRepository, CROrderItemsRepository>();

            services.AddTransient<IMPMarcasDownloadService, MPMarcasDownloadService>();
            services.AddTransient<IMPMarcasManageService, MPMarcasManageService>();
            services.AddTransient<IMPMarcasReaderService, MPMarcasReaderService>();
            services.AddTransient<IMPMarcasUploadService, MPMarcasUploadService>();

            services.AddTransient<IGEQueueItemService, GEQueueItemService>();
            services.AddTransient<ICROrdersService, CROrdersService>();
            services.AddTransient<ICROrderItemsService, CROrderItemsService>();

            services.AddTransient<IMPCategoriasDownloadService, MPCategoriasDownloadService>();
            services.AddTransient<IMPCategoriasManageService, MPCategoriasManageService>();
            services.AddTransient<IMPCategoriasReaderService, MPCategoriasReaderService>();
            services.AddTransient<IMPCategoriasUploadService, MPCategoriasUploadService>();

            services.AddTransient<IMPProductsDownloadService, MPProductsDownloadService>();
            services.AddTransient<IMPProductsManageService, MPProductsManageService>();
            services.AddTransient<IMPProductsReaderService, MPProductsReaderService>();
            services.AddTransient<IMPProductsUploadService, MPProductsUploadService>();

            services.AddTransient<IMPEstoqueManageService, MPEstoqueManageService>();

            services.AddTransient<IMPTiposVariacaoManagerService, MPTiposVariacaoManagerService>();

            services.AddTransient<IMPMarcasManageAS, MPMarcasManageAS>();
            services.AddTransient<IMPCategoriasManageAS, MPCategoriasManageAS>();
            services.AddTransient<IMPProdutosManageAS, MPProdutosManageAS>();
            services.AddTransient<IMPTiposVariacaoAS, MPTiposVariacaoAS>();
            services.AddTransient<IMPEstoqueManageAS, MPEstoqueManageAS>();
            services.AddTransient<IOrderReceivedAS, OrderReceivedAS>();



        }
    }
}
