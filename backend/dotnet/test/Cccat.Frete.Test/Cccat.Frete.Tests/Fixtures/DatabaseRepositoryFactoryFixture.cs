using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Domain.Interfaces;
using Cccat.Frete.Infra.Database;
using Cccat.Frete.Infra.Factories;

namespace Cccat.Frete.Tests.Fixtures
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
			public ICepRepository CriarCepRepository()
				=> new CepRepositoryFake();
		}

		internal class CepRepositoryFake : ICepRepository
		{
			private static List<Cep> Ceps()
				=> new()
				{
					new Cep("17600090","Rua Cherentes","Centro",-21.940867,-50.506929),
					new Cep("72980000","Rua Manoel","Vila Pratinha",-15.855776,-48.955921)
				};

			public void AdicionarCep(Cep cep)
				=> throw new NotImplementedException();

			public Cep ObterPorCodigo(string codigo)
				=> Ceps().Find(cep => cep.Codigo.Equals(codigo));
		}
	}
}
