using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cccat.Infra.Configurations
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
                {
                    opt.UseInMemoryDatabase("CCCAT");
                }
                else
                {
                    opt.UseSqlServer(connectionString);
                }

            }, serviceLifetime);

            return services;
        }
    }
}
