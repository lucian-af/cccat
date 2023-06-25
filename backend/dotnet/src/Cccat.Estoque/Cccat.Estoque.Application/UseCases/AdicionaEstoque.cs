using Cccat.Estoque.Application.Models;
using Cccat.Estoque.Domain.Entities;
using Cccat.Estoque.Domain.Enums;
using Cccat.Estoque.Domain.Interfaces;

namespace Cccat.Estoque.Application.UseCases
{
	public class AdicionaEstoque
	{
		private readonly IFluxoEstoqueRepository _fluxoEstoqueRepository;

		public AdicionaEstoque(IRepositoryFactory factory)
			=> _fluxoEstoqueRepository = factory.CriarFluxoEstoqueRepository();

		public async Task Adicionar(AdicionaEstoqueInputDto input)
		{
			var fluxosEstoque = new List<FluxoEstoque>();
			foreach (var item in input.Itens)
			{
				var fluxoEstoque = new FluxoEstoque(item.IdProduto, TipoOperacao.Entrada, item.Quantidade);
				fluxosEstoque.Add(fluxoEstoque);
			}
			if (!fluxosEstoque.Any())
				throw new ArgumentException("Nenhum produto informado para baixa no estoque.", nameof(input));

			await _fluxoEstoqueRepository.Salvar(fluxosEstoque);
		}
	}
}
