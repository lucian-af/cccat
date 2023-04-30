using Cccat.Infra;
using Cccat.Infra.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Cccat.API.Test.Fixtures
{
    public class CustomWebApiFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));

                services.Remove(descriptor);

                services.AddDatabaseConfiguration(string.Empty, useInMemory: true);
            });

            builder.UseEnvironment("Development");
        }
    }
}
