using Cccat.Autenticacao.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Autenticacao.Infra.Configurations
{
	public static class MappingsConfiguration
	{
		public static void AddMappings(this ModelBuilder modelBuilder)
			=> modelBuilder.ApplyConfiguration(new UsuarioMapping());
	}
}
