using Cccat.Domain.Entities;

namespace Cccat.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        public Produto Get(int idProduto);
    }
}
