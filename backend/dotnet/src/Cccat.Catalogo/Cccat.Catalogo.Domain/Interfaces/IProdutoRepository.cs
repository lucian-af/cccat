using Cccat.Catalogo.Domain.Entities;

namespace Cccat.Catalogo.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        public Produto Get(int idProduto);

        public IEnumerable<Produto> All();
    }
}
