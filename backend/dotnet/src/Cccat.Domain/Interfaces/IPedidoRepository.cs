using Cccat.Domain.Entities;

namespace Cccat.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        public Pedido ConsultarPedidoPorId(Guid idPedido);

        public Pedido ConsultarPedidoPorCodigo(string codigo);

        public IEnumerable<Pedido> ConsultaTodos();

        public Task<long> ObterTotalPedidos();

        public Task AdicionarPedido(Pedido pedido);
    }
}
