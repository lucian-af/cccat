using Cccat.Checkout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Checkout.Infra.Mapping
{
	public class PedidoMapping : IEntityTypeConfiguration<Pedido>
	{
		public void Configure(EntityTypeBuilder<Pedido> builder)
		{
			builder.ToTable("PEDIDOS");
			builder.HasKey(pr => pr.Id);

			builder
				.Property(pr => pr.Codigo)
				.HasColumnType("VARCHAR(20)")
				.IsRequired();

			builder
				.OwnsOne(pr => pr.Cpf)
				.Property(cpf => cpf.Valor)
				.HasColumnName("Cpf")
				.HasColumnType("VARCHAR(11)")
				.IsRequired();

			builder
				.Property(pr => pr.SubTotal)
				.HasColumnType("MONEY")
				.IsRequired();

			builder
				.Property(pr => pr.Total)
				.HasColumnType("MONEY")
				.IsRequired();

			builder
				.Property(pr => pr.Frete)
				.HasColumnType("MONEY")
				.IsRequired();

			builder
				.Property(pr => pr.Desconto)
				.HasColumnType("MONEY")
				.HasDefaultValue(0)
				.IsRequired();

			builder
				.HasOne<Cupom>()
				.WithMany()
				.HasForeignKey(pe => pe.IdCupom)
				.IsRequired(false);

		}
	}
}
