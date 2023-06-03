using Cccat.Checkout.Domain.Entities;

namespace Cccat.Checkout.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        public Produto Get(int idProduto);

        public IEnumerable<Produto> All();
    }
}
