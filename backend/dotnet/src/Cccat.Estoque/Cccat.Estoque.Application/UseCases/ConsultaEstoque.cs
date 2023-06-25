using Cccat.Estoque.Application.Models;
using Cccat.Estoque.Domain.Interfaces;
using Cccat.Estoque.Domain.Services;

namespace Cccat.Estoque.Application.UseCases
{
	public class ConsultaEstoque
	{
		private readonly IFluxoEstoqueRepository _fluxoEstoqueRepository;

		public ConsultaEstoque(IRepositoryFactory factory)
			=> _fluxoEstoqueRepository = factory.CriarFluxoEstoqueRepository();

		public async Task<ConsultaEstoqueOutputDto> Consultar(int idProduto)
		{
			var fluxosEstoque = await _fluxoEstoqueRepository.Consultar(idProduto);
			var total = CalculadoraEstoque.Calcular(fluxosEstoque);
			return new() { Total = total };
		}
	}
}
