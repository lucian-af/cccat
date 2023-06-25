using Cccat.Estoque.Domain.Entities;
using Cccat.Estoque.Domain.Interfaces;
using Cccat.Estoque.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Estoque.Infra.Repositories;
public class FluxoEstoqueRepository : IFluxoEstoqueRepository
{
	private readonly DatabaseContext _context;

	public FluxoEstoqueRepository(DatabaseContext databaseContext)
		=> _context = databaseContext;

	public async Task Salvar(FluxoEstoque fluxoEstoque)
	{
		_context.FluxoEstoque.Add(fluxoEstoque);
		await _context.SaveChangesAsync();
	}

	public async Task Salvar(List<FluxoEstoque> fluxoEstoque)
	{
		_context.FluxoEstoque.AddRange(fluxoEstoque);
		await _context.SaveChangesAsync();
	}

	public async Task<List<FluxoEstoque>> Consultar(int idProduto)
		=> await _context.FluxoEstoque
			.Where(fe => fe.IdProduto == idProduto)
			.AsNoTracking()
			.ToListAsync();
}
