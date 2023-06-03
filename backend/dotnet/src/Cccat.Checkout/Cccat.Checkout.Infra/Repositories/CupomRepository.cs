using Cccat.Checkout.Domain.Entities;
using Cccat.Checkout.Domain.Interfaces;
using Cccat.Checkout.Infra.Database;

namespace Cccat.Checkout.Infra.Repositories
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
