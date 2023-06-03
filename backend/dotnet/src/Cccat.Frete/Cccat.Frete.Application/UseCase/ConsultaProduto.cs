using Cccat.Frete.Application.Models;
using Cccat.Frete.Domain.Interfaces;

namespace Cccat.Frete.Application.UseCase
{
    public class ConsultaProduto
    {
        private readonly IProdutoRepository _produtoRepository;

        public ConsultaProduto(IRepositoryFactory repositoryFactory)
            => _produtoRepository = repositoryFactory.CriarProdutoRepository();

        public IEnumerable<ConsultaProdutoOutputDto> ObterTodos()
        {
            var produtos = _produtoRepository.All();
            return produtos.Select(produto => new ConsultaProdutoOutputDto
            {
                IdProduto = produto.Id,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
            });
        }
    }
}
