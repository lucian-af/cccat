using Cccat.Catalogo.Domain.Entities;
using Cccat.Catalogo.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Catalogo.Infra.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }
        public DbSet<Cep> Ceps { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddMappings();
            base.OnModelCreating(modelBuilder);
        }
    }
}
