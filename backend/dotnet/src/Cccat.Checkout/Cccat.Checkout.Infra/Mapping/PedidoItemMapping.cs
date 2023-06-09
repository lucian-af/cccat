using Cccat.Checkout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Checkout.Infra.Mapping
{
    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder
                .ToTable("PEDIDOITENS")
                .HasKey(pdi => pdi.Id);

            builder
                .Property(pr => pr.Preco)
                .HasColumnType("MONEY")
                .IsRequired();

            builder
                .ToTable(t => t.HasCheckConstraint("Ck_Valor", $"{nameof(PedidoItem.Preco)} > 0"));

            builder
                .Property(pr => pr.Quantidade)
                .HasColumnType("INTEGER")
                .IsRequired();

            builder
                .ToTable(t => t.HasCheckConstraint("Ck_Quantidade", $"{nameof(PedidoItem.Quantidade)} > 0"));

            builder
                .HasOne<Pedido>()
                .WithMany(pdi => pdi.Itens)
                .HasForeignKey(e => e.IdPedido)
                .IsRequired();

            builder
                .HasIndex(pdi => new { pdi.IdPedido, pdi.IdProduto })
                .IsUnique();
        }
    }
}
