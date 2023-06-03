using Cccat.Frete.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Frete.Infra.Mapping
{
    public class CupomMapping : IEntityTypeConfiguration<Cupom>
    {
        public void Configure(EntityTypeBuilder<Cupom> builder)
        {
            builder.ToTable("CUPOM");

            builder
                .Property(pr => pr.Id)
                .HasColumnType("INTEGER")
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .Property(pr => pr.Codigo)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            builder
                .Property(pr => pr.Percentual)
                .HasColumnType("DECIMAL(14,2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder
                .Property(pr => pr.Validade)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder
                .HasMany<Pedido>()
                .WithOne(pr => pr.Cupom)
                .HasForeignKey("IdCupom")
                .IsRequired(false);
        }
    }
}
