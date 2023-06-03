﻿using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Domain.Interfaces;
using Cccat.Frete.Infra.Database;

namespace Cccat.Frete.Infra.Repositories
{
    public class CupomRepository : ICupomRepository
    {
        private readonly DatabaseContext _context;

        public CupomRepository(DatabaseContext context)
            => _context = context;

        public Cupom Get(string codigo)
            => _context.Cupons.FirstOrDefault(cp => cp.Codigo.Equals(codigo));
    }
}
