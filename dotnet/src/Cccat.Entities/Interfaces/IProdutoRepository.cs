namespace Cccat.Entities.Interfaces
{
    public interface IProdutoRepository
    {
        public Produto Get(int idProduto);

        public IEnumerable<Produto> All();
    }
}
