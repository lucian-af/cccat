using Cccat.Autenticacao.Domain.Entities;
using Cccat.Autenticacao.Domain.Interfaces;
using Cccat.Autenticacao.Infra.Database;
using Cccat.Autenticacao.Infra.Factories;

namespace Cccat.Autenticacao.Tests.Fixtures
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
			public IUsuarioRepository CriarUsuarioRepository()
				=> new UsuarioRepositoryFake();
		}

		internal class UsuarioRepositoryFake : IUsuarioRepository
		{
			private readonly List<Usuario> Usuarios = new();

			public async Task Cadastrar(Usuario usuario)
			{
				Usuarios.Add(usuario);
				await Task.CompletedTask;
			}

			public async Task<Usuario> ObterUsuarioPorEmail(string email)
				=> await Task.FromResult(Usuarios.Find(us => us.Email.Equals(email)));
		}
	}
}
