using Cccat.Entities;
using Cccat.Entities.Interfaces;

namespace Cccat.UseCases
{
    public class ConsultaPedido
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ConsultaPedido(IPedidoRepository pedidoRepository)
            => _pedidoRepository = pedidoRepository;

        public Pedido ConsultaPorId(Guid idPedido)
            => _pedidoRepository.ConsultarPedidoPorId(idPedido);

        public Pedido ConsultaPorCodigo(string codigo)
            => _pedidoRepository.ConsultarPedidoPorCodigo(codigo);

        public IEnumerable<Pedido> ConsultaTodos()
            => _pedidoRepository.ConsultaTodos();
    }
}
