using Cccat.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Infra.Mapping
{
    public class CepMapping : IEntityTypeConfiguration<Cep>
    {
        public void Configure(EntityTypeBuilder<Cep> builder)
        {
            builder
                .ToTable("CEP")
                .HasKey(cep => cep.Id);

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
                .Property(pr => pr.Latitude)
                .HasColumnType("DECIMAL(10,8)")
                .IsRequired();

            builder
                .Property(pr => pr.Longitude)
                .HasColumnType("DECIMAL(10,8)")
                .IsRequired();
        }
    }
}
