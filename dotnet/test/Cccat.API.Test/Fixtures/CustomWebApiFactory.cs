using Cccat.Infra;
using Cccat.Infra.Configurations;
using Cccat.Infra.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cccat.API.Test.Fixtures
{
    public class CustomWebApiFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Staging");
            Environment.SetEnvironmentVariable("Conexao", configuration.GetConnectionString("Conexao"));

            builder.ConfigureServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
                services.Remove(descriptor);

                var conn = Environment.GetEnvironmentVariable("Conexao");
                services.AddDatabaseConfiguration(conn);
            });

            builder.UseEnvironment("Staging");
            var host = builder.Start();

            using var serviceScope = host.Services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

            dbContext.Database.EnsureDeletedAsync().Wait();
            dbContext.Database.EnsureCreatedAsync().Wait();

            SeedData.CriarDados(dbContext).Wait();

            return host;
        }
    }
}
