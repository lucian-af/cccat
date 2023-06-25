using Cccat.Estoque.Domain.Entities;
using Cccat.Estoque.Domain.Interfaces;
using Cccat.Estoque.Infra.Database;
using Cccat.Estoque.Infra.Factories;

namespace Cccat.Estoque.Tests.Fixtures
{
	public class DatabaseRepositoryFactoryFixture
	{
		public readonly DatabaseContext DbContext;

		private bool Fake { get; set; } = false;

		public DatabaseRepositoryFactoryFixture() { }

		public DatabaseRepositoryFactoryFixture(DatabaseContext dbContext)
			=> DbContext = dbContext;

		public IRepositoryFactory CriarRepositoryFactory()
		{
			if (Fake)
				return new DatabaseRepositoryFactoryFake();

			return new RepositoryFactory(DbContext);
		}

		internal class DatabaseRepositoryFactoryFake : IRepositoryFactory
		{
			public IFluxoEstoqueRepository CriarFluxoEstoqueRepository()
				=> new FluxoEstoqueRepository();
		}

		internal class FluxoEstoqueRepository : IFluxoEstoqueRepository
		{
			private readonly List<FluxoEstoque> _fluxosEstoque = new();

			public Task<List<FluxoEstoque>> Consultar(int idProduto)
				=> Task.FromResult(_fluxosEstoque.Where(fe => fe.IdProduto == idProduto).ToList());

			public Task Salvar(FluxoEstoque fluxoEstoque)
			{
				_fluxosEstoque.Add(fluxoEstoque);
				return Task.CompletedTask;
			}

			public Task Salvar(List<FluxoEstoque> fluxoEstoque)
			{
				_fluxosEstoque.AddRange(fluxoEstoque);
				return Task.CompletedTask;
			}
		}
	}
}
