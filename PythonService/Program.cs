using PythonService;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService() // Habilita o suporte a servi�o do Windows
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();