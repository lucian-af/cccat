using Cccat.Infra;
using Cccat.Infra.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Cccat.API.Test.Fixtures
{
    public class CustomWebApiFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            Environment.SetEnvironmentVariable("Conexao", configuration.GetConnectionString("Conexao"));

            builder.ConfigureServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
                services.Remove(descriptor);

                var conn = Environment.GetEnvironmentVariable("Conexao");

                services.AddDatabaseConfiguration(conn);
            });

            builder.UseEnvironment("Test");
        }
    }
}
