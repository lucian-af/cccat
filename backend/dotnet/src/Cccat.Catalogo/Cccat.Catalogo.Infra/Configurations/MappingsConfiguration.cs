using Cccat.Catalogo.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Catalogo.Infra.Configurations
{
    public static class MappingsConfiguration
    {
        public static void AddMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMapping());
        }
    }
}
