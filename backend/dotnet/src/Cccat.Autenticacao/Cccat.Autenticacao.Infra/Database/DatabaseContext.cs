using Cccat.Autenticacao.Domain.Entities;
using Cccat.Autenticacao.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Autenticacao.Infra.Database
{
	public class DatabaseContext : DbContext
	{
		public DbSet<Usuario> Usuarios { get; set; }

		public DatabaseContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AddMappings();
			base.OnModelCreating(modelBuilder);
		}
	}
}
