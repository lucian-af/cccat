using Cccat.Entities.Interfaces;

namespace Cccat.Infra.Repositories
{
    public class DatabaseRepositoryFactory : IRepositoryFactory
    {
        private readonly DatabaseContext _context;

        public DatabaseRepositoryFactory(DatabaseContext context)
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
