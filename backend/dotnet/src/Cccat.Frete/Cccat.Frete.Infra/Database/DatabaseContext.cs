﻿using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Frete.Infra.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Cep> Ceps { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddMappings();
            base.OnModelCreating(modelBuilder);
        }
    }
}
