using Cccat.Entities.Negocio;

namespace Cccat.Entities.Interfaces
{
    public interface IProdutoRepository
    {
        public Produto Get(int idProduto);
    }
}
