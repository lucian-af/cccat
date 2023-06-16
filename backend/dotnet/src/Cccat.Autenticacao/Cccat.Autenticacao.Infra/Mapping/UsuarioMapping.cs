using Cccat.Autenticacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Autenticacao.Infra.Mapping;
public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
	public void Configure(EntityTypeBuilder<Usuario> builder)
	{
		builder.ToTable("USUARIO");
		builder.HasKey(pr => pr.Id);

		builder
			.OwnsOne(us => us.Email)
			.Property(em => em.Valor)
			.HasColumnName(nameof(Usuario.Email))
			.HasColumnType("VARCHAR(60)")
			.IsRequired();

		builder
			.OwnsOne(us => us.Email)
			.HasIndex(em => em.Valor)
			.IsUnique();

		builder
			.Property(pr => pr.Senha)
			.HasColumnType("VARCHAR(MAX)")
			.IsRequired();

		builder
			.Property(pr => pr.Salt)
			.HasColumnType("VARCHAR(MAX)")
			.IsRequired();

		//builder
		//	.OwnsOne(pr => pr.Dimensao)
		//	.Property(pd => pd.Largura)
		//	.HasColumnName(nameof(ProdutoDimensao.Largura))
		//	.HasColumnType("DECIMAL(14,2)")
		//	.HasDefaultValue(0)
		//	.IsRequired();		
	}
}
