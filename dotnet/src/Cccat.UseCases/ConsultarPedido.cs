using Cccat.Entities;
using Cccat.Entities.Interfaces;

namespace Cccat.UseCases
{
    public class ConsultarPedido
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ConsultarPedido(IPedidoRepository pedidoRepository)
            => _pedidoRepository = pedidoRepository;

        public Pedido Executar(Guid idPedido)
            => (Pedido)_pedidoRepository.ConsultarPedidoPorId(idPedido);
    }
}
