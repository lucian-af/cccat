using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository, IDisposable
    {
        private readonly DatabaseContext _context;

        public PedidoRepository(DatabaseContext context)
            => _context = context;

        public Pedido ConsultarPedidoPorId(Guid idPedido)
            => _context.Pedidos.Find(idPedido);

        public async Task<long> ObterTotalPedidos()
            => await _context.Pedidos.CountAsync();

        public async Task AdicionarPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
            => _context.Dispose();

        ~PedidoRepository()
            => Dispose(false);
    }
}
