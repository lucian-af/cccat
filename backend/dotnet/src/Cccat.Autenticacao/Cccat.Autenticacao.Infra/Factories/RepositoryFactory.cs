using Cccat.Autenticacao.Domain.Interfaces;
using Cccat.Autenticacao.Infra.Database;
using Cccat.Autenticacao.Infra.Repositories;

namespace Cccat.Autenticacao.Infra.Factories
{
	public class RepositoryFactory : IRepositoryFactory
	{
		private readonly DatabaseContext _context;

		public RepositoryFactory(DatabaseContext context)
			=> _context = context;

		public IUsuarioRepository CriarUsuarioRepository()
			=> new UsuarioRepository(_context);
	}
}
