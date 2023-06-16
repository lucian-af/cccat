using Cccat.Autenticacao.Application.UseCases;
using Cccat.Autenticacao.Domain.Interfaces;

namespace Cccat.Autenticacao.Application.Factories
{
	public class UseCaseFactory
	{
		private readonly IRepositoryFactory _repositoryFactory;

		public UseCaseFactory(IRepositoryFactory repositoryFactory)
			=> _repositoryFactory = repositoryFactory;

		public CadastraUsuario CriarCadastraUsuario()
			=> new(_repositoryFactory);
	}
}
