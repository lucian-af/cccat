using Cccat.Frete.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Frete.Infra.Configurations
{
    public static class MappingsConfiguration
    {
        public static void AddMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMapping());
            modelBuilder.ApplyConfiguration(new CupomMapping());
            modelBuilder.ApplyConfiguration(new PedidoMapping());
            modelBuilder.ApplyConfiguration(new PedidoItemMapping());
            modelBuilder.ApplyConfiguration(new CepMapping());
        }
    }
}
