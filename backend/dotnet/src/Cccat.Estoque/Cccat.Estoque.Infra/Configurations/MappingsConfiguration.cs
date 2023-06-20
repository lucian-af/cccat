using Cccat.Estoque.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Estoque.Infra.Configurations
{
	public static class MappingsConfiguration
	{
		public static void AddMappings(this ModelBuilder modelBuilder)
			=> modelBuilder.ApplyConfiguration(new FluxoEstoqueMapping());
	}
}
