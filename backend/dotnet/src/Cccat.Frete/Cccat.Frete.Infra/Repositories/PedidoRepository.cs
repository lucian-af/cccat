using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Domain.Interfaces;
using Cccat.Frete.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Frete.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DatabaseContext _context;

        public PedidoRepository(DatabaseContext context)
            => _context = context;

        public Pedido ConsultarPedidoPorId(Guid idPedido)
            => _context.Pedidos.Find(idPedido);

        public Pedido ConsultarPedidoPorCodigo(string codigo)
            => _context.Pedidos.FirstOrDefault(pedido => pedido.Codigo.Equals(codigo));

        public IEnumerable<Pedido> ConsultaTodos()
            => _context.Pedidos.AsNoTracking().AsEnumerable();

        public async Task<long> ObterTotalPedidos()
            => await _context.Pedidos.CountAsync();

        public async Task AdicionarPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
