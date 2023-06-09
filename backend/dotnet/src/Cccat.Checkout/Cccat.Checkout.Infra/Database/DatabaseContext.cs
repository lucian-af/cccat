using Cccat.Checkout.Domain.Entities;
using Cccat.Checkout.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Checkout.Infra.Database
{
	public class DatabaseContext : DbContext
	{
		public DbSet<Cupom> Cupons { get; set; }
		public DbSet<Pedido> Pedidos { get; set; }
		public DbSet<PedidoItem> PedidoItens { get; set; }

		public DatabaseContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AddMappings();
			base.OnModelCreating(modelBuilder);
		}
	}
}
