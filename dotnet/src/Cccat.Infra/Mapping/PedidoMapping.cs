using Cccat.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Infra.Mapping
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("PEDIDOS");

            builder
                .Property(pr => pr.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .Property(pr => pr.Codigo)
                .HasColumnType("VARCHAR(20)")
                .IsRequired();

            builder
                .Property(pr => pr.Cpf)
                .HasColumnType("VARCHAR(11)")
                .IsRequired();

            builder
                .Property(pr => pr.Total)
                .HasColumnType("MONEY")
                .IsRequired();

            builder
                .Property(pr => pr.Frete)
                .HasColumnType("MONEY")
                .IsRequired();
        }
    }
}
