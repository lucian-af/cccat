using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Domain.Interfaces;
using Cccat.Frete.Infra.Database;

namespace Cccat.Frete.Infra.Repositories
{
    public class CepRepository : ICepRepository
    {
        private readonly DatabaseContext _context;

        public CepRepository(DatabaseContext context)
            => _context = context;

        public Cep Get(string codigo)
            => _context.Ceps.FirstOrDefault(cp => cp.Codigo.Equals(codigo));

        public void AdicionarCep(Cep cep)
        {
            _context.Ceps.Add(cep);
            _context.SaveChanges();
        }
    }
}
