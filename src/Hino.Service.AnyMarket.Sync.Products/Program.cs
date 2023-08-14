using Hino.Service.AnyMarket.Sync.Products;
using Hino.Service.AnyMarket.IoC;
using Hino.Service.AnyMarket.Sync.Core.Configurations;
using Hino.Service.AnyMarket.DataBase.ContextDB;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hcontext, services) =>
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

        services.AddHostedService<Worker>();
        services.RegisterServices();
    })
    .Build();

await host.RunAsync();
