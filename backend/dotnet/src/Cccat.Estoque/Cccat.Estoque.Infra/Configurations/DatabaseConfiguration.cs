using Cccat.Estoque.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cccat.Estoque.Infra.Configurations
{
	public static class DatabaseConfiguration
	{
		public static IServiceCollection AddDatabaseConfiguration(
			this IServiceCollection services,
			string connectionString,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped,
			bool useInMemory = false)
		{
			if (!useInMemory)
				ArgumentNullException.ThrowIfNull(nameof(connectionString));

			services.AddDbContext<DatabaseContext>(opt =>
			{
				if (useInMemory)
					opt.UseInMemoryDatabase("Cccat");
				else
					opt.UseSqlServer(connectionString);

			}, serviceLifetime);

			return services;
		}
	}
}
