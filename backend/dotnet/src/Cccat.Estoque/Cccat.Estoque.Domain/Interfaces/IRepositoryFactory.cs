namespace Cccat.Estoque.Domain.Interfaces
{
	public interface IRepositoryFactory
	{
		public IFluxoEstoqueRepository CriarFluxoEstoqueRepository();
	}
}
