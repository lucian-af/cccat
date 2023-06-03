using Cccat.Frete.Domain.Entities;

namespace Cccat.Frete.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        public Produto Get(int idProduto);

        public IEnumerable<Produto> All();
    }
}
