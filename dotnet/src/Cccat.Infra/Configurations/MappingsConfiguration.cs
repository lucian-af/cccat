using Cccat.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Infra.Configurations
{
    public static class MappingsConfiguration
    {
        public static void AddMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMapping());
            modelBuilder.ApplyConfiguration(new CupomMapping());
        }
    }
}
