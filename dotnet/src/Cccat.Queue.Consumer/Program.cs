using Cccat.Infra;
using Cccat.Infra.Configurations;
using Cccat.Queue.Consumer;
using Cccat.Queue.Consumer.Helpers;
using Cccat.UseCases;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<DatabaseContext>();
        services.AddSingleton<Checkout>();

        services.AddDatabaseConfiguration(string.Empty, useInMemory: true, serviceLifetime: ServiceLifetime.Singleton);
    })
    .Build();

host.ExecutarSeedDados().Wait();
host.Run();