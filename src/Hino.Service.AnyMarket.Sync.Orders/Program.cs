using Hino.Service.AnyMarket.IoC;
using Hino.Service.AnyMarket.Sync.Orders;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

        services.AddHostedService<Worker>();
        services.RegisterServices();
    })
    .Build();

await host.RunAsync();
