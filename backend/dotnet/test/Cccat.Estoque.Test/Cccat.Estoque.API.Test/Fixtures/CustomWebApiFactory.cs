using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Cccat.Estoque.BackgroundTask.Settings;
using Cccat.Estoque.Infra.Configurations;
using Cccat.Estoque.Infra.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Respawn;

namespace Cccat.Estoque.API.Test.Fixtures
{
	public class CustomWebApiFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
	{
		private Respawner Respawner;
		private string ConnectionString;

		public Task LimparBase()
			=> Respawner.ResetAsync(ConnectionString);

		protected override IHost CreateHost(IHostBuilder builder)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();

			ConnectionString = configuration.GetConnectionString("Conexao");

			Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Staging");
			Environment.SetEnvironmentVariable("Conexao", ConnectionString);

			builder.ConfigureServices(services =>
			{
				var descriptor = services
					.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
				services.Remove(descriptor);

				var conn = Environment.GetEnvironmentVariable("Conexao");
				services.AddDatabaseConfiguration(conn);
				services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));
			});

			builder.UseEnvironment("Staging");
			var host = builder.Start();

			using var serviceScope = host.Services.CreateScope();
			var databaseContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

			databaseContext.Database.EnsureCreatedAsync().Wait();

			Respawner = Respawner.CreateAsync(ConnectionString).GetAwaiter().GetResult();
			Respawner.ResetAsync(ConnectionString).Wait();

			return host;
		}

		public string GerarToken()
		{
			var chave = "<um-super-segredo>";

			var hashKey = Encoding.ASCII.GetBytes(chave);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Expires = DateTime.Now.AddMinutes(10).ToUniversalTime(),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(hashKey), SecurityAlgorithms.HmacSha256)
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}
	}
}
