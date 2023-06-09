using Cccat.Checkout.Domain.Interfaces;
using Cccat.Checkout.Infra.Database;
using Cccat.Checkout.Infra.Repositories;

namespace Cccat.Checkout.Infra.Factories
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
    }
}
