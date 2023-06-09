using Cccat.Catalogo.Application.Models;
using Cccat.Catalogo.Domain.Interfaces;

namespace Cccat.Catalogo.Application.UseCase
{
	public class ConsultaProduto
	{
		private readonly IProdutoRepository _produtoRepository;

		public ConsultaProduto(IRepositoryFactory repositoryFactory)
			=> _produtoRepository = repositoryFactory.CriarProdutoRepository();

		public ConsultaProdutoOutputDto Buscar(int idProduto)
		{
			var produto = _produtoRepository.Get(idProduto);
			return new ConsultaProdutoOutputDto
			{
				IdProduto = produto.Id,
				Descricao = produto.Descricao,
				Preco = produto.Preco,
				Altura = produto.Dimensao.Altura,
				Largura = produto.Dimensao.Largura,
				Profundidade = produto.Dimensao.Profundidade,
				Peso = produto.Peso,
				Densidade = produto.Densidade(),
				Volume = produto.Volume()
			};
		}
	}
}
