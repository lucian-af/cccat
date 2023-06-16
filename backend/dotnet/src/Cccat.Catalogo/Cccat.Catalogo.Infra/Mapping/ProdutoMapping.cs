using Cccat.Catalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Catalogo.Infra.Mapping
{
	public class ProdutoMapping : IEntityTypeConfiguration<Produto>
	{
		public void Configure(EntityTypeBuilder<Produto> builder)
		{
			builder
				.ToTable("PRODUTO")
				.HasIndex(pr => pr.Id);

			builder
				.Property(pr => pr.Id)
				.HasColumnType("INTEGER")
				.IsRequired()
				.ValueGeneratedNever();

			builder
				.Property(pr => pr.Descricao)
				.HasColumnType("VARCHAR(255)")
				.IsRequired();

			builder
				.Property(pr => pr.Preco)
				.HasColumnType("DECIMAL(14,2)")
				.IsRequired();

			builder
				.OwnsOne(pr => pr.Dimensao)
				.Property(pd => pd.Largura)
				.HasColumnName(nameof(ProdutoDimensao.Largura))
				.HasColumnType("DECIMAL(14,2)")
				.HasDefaultValue(0)
				.IsRequired();

			builder
				.OwnsOne(pr => pr.Dimensao)
				.Property(pd => pd.Altura)
				.HasColumnName(nameof(ProdutoDimensao.Altura))
				.HasColumnType("DECIMAL(14,2)")
				.HasDefaultValue(0)
				.IsRequired();

			builder
				.OwnsOne(pr => pr.Dimensao)
				.Property(pd => pd.Profundidade)
				.HasColumnName(nameof(ProdutoDimensao.Profundidade))
				.HasColumnType("DECIMAL(14,2)")
				.HasDefaultValue(0)
				.IsRequired();

			builder
				.Property(pr => pr.Peso)
				.HasColumnType("DECIMAL(14,2)")
				.HasDefaultValue(0)
				.IsRequired();
		}
	}
}
