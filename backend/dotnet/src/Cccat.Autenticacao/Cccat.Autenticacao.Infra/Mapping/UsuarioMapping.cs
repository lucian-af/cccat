using Cccat.Autenticacao.Infra.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Autenticacao.Infra.Mapping;
public class UsuarioMapping : IEntityTypeConfiguration<UsuarioDb>
{
	public void Configure(EntityTypeBuilder<UsuarioDb> builder)
	{
		builder.ToTable("USUARIO");
		builder.HasKey(us => us.Id);

		builder
			.Property(us => us.Email)
			.HasColumnType("VARCHAR(60)")
			.IsRequired();

		builder
			.HasIndex(em => em.Email)
			.IsUnique();

		builder
			.Property(us => us.Senha)
			.HasColumnType("VARCHAR(MAX)")
			.IsRequired();

		builder
			.Property(us => us.Salt)
			.HasColumnType("VARCHAR(MAX)");

		builder
			.Property(us => us.SenhaTipo)
			.HasColumnType("TINYINT")
			.IsRequired();
	}
}
