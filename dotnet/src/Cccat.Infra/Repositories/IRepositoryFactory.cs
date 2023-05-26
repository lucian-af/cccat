using Cccat.Entities.Interfaces;

namespace Cccat.Infra.Repositories
{
    public interface IRepositoryFactory
    {
        public IProdutoRepository CriarProdutoRepository();
        public IPedidoRepository CriarPedidoRepository();
        public ICupomRepository CriarCupomRepository();
        public ICepRepository CriarCepRepository();
    }
}
