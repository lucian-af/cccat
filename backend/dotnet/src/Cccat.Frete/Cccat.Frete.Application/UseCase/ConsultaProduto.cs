using Cccat.Application.Models;
using Cccat.Domain.Interfaces;

namespace Cccat.Application.UseCase
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
