namespace Cccat.Checkout.Domain.Interfaces
{
    public interface IRepositoryFactory
    {
        public IProdutoRepository CriarProdutoRepository();
        public IPedidoRepository CriarPedidoRepository();
        public ICupomRepository CriarCupomRepository();
        public ICepRepository CriarCepRepository();
    }
}
