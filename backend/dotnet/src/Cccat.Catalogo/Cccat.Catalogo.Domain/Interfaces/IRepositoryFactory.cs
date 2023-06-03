namespace Cccat.Catalogo.Domain.Interfaces
{
    public interface IRepositoryFactory
    {
        public IProdutoRepository CriarProdutoRepository();
    }
}
