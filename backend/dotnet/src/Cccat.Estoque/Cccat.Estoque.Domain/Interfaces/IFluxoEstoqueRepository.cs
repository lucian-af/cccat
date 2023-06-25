using Cccat.Estoque.Domain.Entities;

namespace Cccat.Estoque.Domain.Interfaces;
public interface IFluxoEstoqueRepository
{
	public Task Salvar(FluxoEstoque fluxoEstoque);

	public Task Salvar(List<FluxoEstoque> fluxoEstoque);

	public Task<List<FluxoEstoque>> Consultar(int idProduto);
}
