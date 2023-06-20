using Cccat.Estoque.Domain.Interfaces;
using Cccat.Estoque.Infra.Database;
using Cccat.Estoque.Infra.Repositories;

namespace Cccat.Estoque.Infra.Factories
{
	public class RepositoryFactory : IRepositoryFactory
	{
		private readonly DatabaseContext _context;

		public RepositoryFactory(DatabaseContext context)
			=> _context = context;

		public IFluxoEstoqueRepository CriarFluxoEstoqueRepository()
			=> new FluxoEstoqueRepository(_context);
	}
}
