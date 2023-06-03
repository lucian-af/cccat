using Cccat.Catalogo.Domain.Entities;

namespace Cccat.Catalogo.Domain.Interfaces
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
