using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Domain.Interfaces;

namespace Cccat.Frete.Application.UseCase
{
    public class ConsultaPedido
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ConsultaPedido(IRepositoryFactory repositoryFactory)
            => _pedidoRepository = repositoryFactory.CriarPedidoRepository();

        public Pedido ConsultaPorId(Guid idPedido)
            => _pedidoRepository.ConsultarPedidoPorId(idPedido);

        public Pedido ConsultaPorCodigo(string codigo)
            => _pedidoRepository.ConsultarPedidoPorCodigo(codigo);

        public IEnumerable<Pedido> ConsultaTodos()
            => _pedidoRepository.ConsultaTodos();
    }
}
