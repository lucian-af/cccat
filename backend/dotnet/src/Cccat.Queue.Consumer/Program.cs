IHost host = Host.CreateDefaultBuilder(args)
    //.ConfigureServices(services =>
    //{
    //    services.AddHostedService<Worker>();
    //    services.AddSingleton<DatabaseContext>();
    //    services.AddSingleton<Checkout>();

    //    services.AddDatabaseConfiguration(string.Empty, useInMemory: true, serviceLifetime: ServiceLifetime.Singleton);
    //})
    .Build();

//host.ExecutarSeedDados().Wait();
host.Run();