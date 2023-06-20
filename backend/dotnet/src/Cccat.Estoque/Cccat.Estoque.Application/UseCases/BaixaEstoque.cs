using Cccat.Estoque.Application.Models;
using Cccat.Estoque.Domain.Interfaces;

namespace Cccat.Estoque.Application.UseCases
{
	public class BaixaEstoque
	{
		private readonly IFluxoEstoqueRepository _fluxoEstoqueRepository;

		public BaixaEstoque(IRepositoryFactory factory)
			=> _fluxoEstoqueRepository = factory.CriarFluxoEstoqueRepository();

		public static void Exec(BaixaEstoqueInputDto input) { }
	}
}
