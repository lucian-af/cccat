using Cccat.Entities;
using Cccat.Entities.Interfaces;

namespace Cccat.Infra.Repositories
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
