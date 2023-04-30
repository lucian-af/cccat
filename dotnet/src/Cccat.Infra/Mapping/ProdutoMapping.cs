using Cccat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Infra.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder
                .ToTable("PRODUTO")
                .HasKey(pr => pr.Id);

            builder
                .Property(pr => pr.Descricao)
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            builder
                .Property(pr => pr.Preco)
                .HasColumnType("DECIMAL(14,2)")
                .IsRequired();

            builder
                .Property(pr => pr.Largura)
                .HasColumnType("DECIMAL(14,2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder
                .Property(pr => pr.Altura)
                .HasColumnType("DECIMAL(14,2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder
                .Property(pr => pr.Profundidade)
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
