using Cccat.Catalogo.Domain.Interfaces;
using Cccat.Catalogo.Infra.Database;
using Cccat.Catalogo.Infra.Repositories;

namespace Cccat.Catalogo.Infra.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DatabaseContext _context;

        public RepositoryFactory(DatabaseContext context)
            => _context = context;

        public IProdutoRepository CriarProdutoRepository()
            => new ProdutoRepository(_context);
    }
}
