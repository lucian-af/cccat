﻿using Cccat.Entities.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cccat.Infra.Mapping
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
        }
    }
}
