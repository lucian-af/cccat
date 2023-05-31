using Cccat.Domain.Interfaces;
using Cccat.Infra.Database;
using Cccat.Infra.Repositories;

namespace Cccat.Infra.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DatabaseContext _context;

        public RepositoryFactory(DatabaseContext context)
            => _context = context;

        public ICepRepository CriarCepRepository()
            => new CepRepository(_context);

        public ICupomRepository CriarCupomRepository()
            => new CupomRepository(_context);

        public IPedidoRepository CriarPedidoRepository()
            => new PedidoRepository(_context);

        public IProdutoRepository CriarProdutoRepository()
            => new ProdutoRepository(_context);
    }
}
