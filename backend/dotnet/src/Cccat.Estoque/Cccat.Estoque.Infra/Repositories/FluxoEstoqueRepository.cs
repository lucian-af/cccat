using Cccat.Estoque.Domain.Interfaces;
using Cccat.Estoque.Infra.Database;

namespace Cccat.Estoque.Infra.Repositories;
public class FluxoEstoqueRepository : IFluxoEstoqueRepository
{
	private readonly DatabaseContext _context;

	public FluxoEstoqueRepository(DatabaseContext databaseContext)
		=> _context = databaseContext;
}
