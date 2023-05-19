namespace Cccat.Entities.Interfaces
{
    public interface IPedidoRepository
    {
        public Pedido ConsultarPedidoPorId(Guid idPedido);

        public Task<long> ObterTotalPedidos();

        public Task AdicionarPedido(Pedido pedido);
    }
}
