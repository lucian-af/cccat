using Cccat.Estoque.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Estoque.Infra.Mapping;
public class FluxoEstoqueMapping : IEntityTypeConfiguration<FluxoEstoque>
{
	public void Configure(EntityTypeBuilder<FluxoEstoque> builder)
	{
		builder.ToTable("FluxoEstoque", t
			=> t.HasCheckConstraint("chk_Operacao", $"{nameof(FluxoEstoque.Operacao)} in (1,2)"));

		builder.HasKey(us => us.Id);

		builder
			.Property(pr => pr.IdProduto)
			.HasColumnType("INTEGER")
			.IsRequired();

		builder
			.Property(pr => pr.Operacao)
			.HasColumnType("TINYINT")
			.IsRequired();

		builder
			.Property(pr => pr.Quantidade)
			.HasColumnType("INTEGER")
			.IsRequired();

		builder.HasIndex(pr => pr.IdProduto);
	}
}
