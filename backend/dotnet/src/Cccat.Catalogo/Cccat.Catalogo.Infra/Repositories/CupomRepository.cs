using Cccat.Catalogo.Domain.Entities;
using Cccat.Catalogo.Domain.Interfaces;
using Cccat.Catalogo.Infra.Database;

namespace Cccat.Catalogo.Infra.Repositories
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
