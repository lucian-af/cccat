using Cccat.Frete.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Frete.Infra.Mapping
{
	public class CepMapping : IEntityTypeConfiguration<Cep>
	{
		public void Configure(EntityTypeBuilder<Cep> builder)
		{
			builder
				.ToTable("CEP")
				.HasIndex(cep => cep.Id);

			builder
				.Property(pr => pr.Codigo)
				.HasColumnType("CHAR(8)")
				.IsRequired();

			builder
				.Property(pr => pr.Rua)
				.HasColumnType("VARCHAR(100)")
				.IsRequired();

			builder
				.Property(pr => pr.Bairro)
				.HasColumnType("VARCHAR(100)")
				.IsRequired();

			builder
				.OwnsOne(pr => pr.Coordenadas)
				.Property(pr => pr.Latitude)
				.HasColumnName(nameof(Coordenada.Latitude))
				.HasColumnType("DECIMAL(10,8)")
				.IsRequired();

			builder
				.OwnsOne(pr => pr.Coordenadas)
				.Property(pr => pr.Longitude)
				.HasColumnName(nameof(Coordenada.Longitude))
				.HasColumnType("DECIMAL(10,8)")
				.IsRequired();
		}
	}
}
