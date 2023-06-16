using Cccat.Autenticacao.Infra.Configurations;
using Cccat.Autenticacao.Infra.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Respawn;

namespace Cccat.Autenticacao.API.Test.Fixtures
{
	public class CustomWebApiFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
	{
		protected override IHost CreateHost(IHostBuilder builder)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();

			var connectionString = configuration.GetConnectionString("Conexao");

			Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Staging");
			Environment.SetEnvironmentVariable("Conexao", connectionString);

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

			var resp = Respawner.CreateAsync(connectionString).GetAwaiter().GetResult();
			resp.ResetAsync(connectionString).Wait();

			return host;
		}
	}
}
