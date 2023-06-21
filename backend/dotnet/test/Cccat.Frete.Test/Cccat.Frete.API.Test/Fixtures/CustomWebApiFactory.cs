using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Cccat.Frete.Infra.Configurations;
using Cccat.Frete.Infra.Database;
using Cccat.Frete.Infra.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Cccat.Frete.API.Test.Fixtures
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
