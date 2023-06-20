using Cccat.Estoque.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Estoque.Infra.Mapping;
public class FluxoEstoqueMapping : IEntityTypeConfiguration<FluxoEstoque>
{
	public void Configure(EntityTypeBuilder<FluxoEstoque> builder)
	{
		builder.ToTable("FluxoEstoque");
		builder.HasKey(us => us.Id);
	}
}
