using Cccat.Autenticacao.Infra.Configurations;
using Cccat.Autenticacao.Infra.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Autenticacao.Infra.Database
{
	public class DatabaseContext : DbContext
	{
		public DbSet<UsuarioDb> Usuarios { get; set; }

		public DatabaseContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AddMappings();
			base.OnModelCreating(modelBuilder);
		}
	}
}
