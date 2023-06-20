using Cccat.Estoque.Domain.Entities;
using Cccat.Estoque.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Estoque.Infra.Database
{
	public class DatabaseContext : DbContext
	{
		public DbSet<FluxoEstoque> FluxoEstoque { get; set; }

		public DatabaseContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AddMappings();
			base.OnModelCreating(modelBuilder);
		}
	}
}
