using Cccat.Catalogo.Application.Models;
using Cccat.Catalogo.Domain.Interfaces;

namespace Cccat.Catalogo.Application.UseCase
{
    public class ConsultaProdutos
    {
        private readonly IProdutoRepository _produtoRepository;

        public ConsultaProdutos(IRepositoryFactory repositoryFactory)
            => _produtoRepository = repositoryFactory.CriarProdutoRepository();

        public IEnumerable<ConsultaProdutosOutputDto> Buscar()
        {
            var produtos = _produtoRepository.All();
            return produtos.Select(produto => new ConsultaProdutosOutputDto
            {
                IdProduto = produto.Id,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
            });
        }
    }
}
