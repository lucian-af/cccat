using Cccat.Estoque.Application.UseCases;
using Cccat.Estoque.Domain.Interfaces;

namespace Cccat.Estoque.Application.Factories
{
	public class UseCaseFactory
	{
		private readonly IRepositoryFactory _repositoryFactory;

		public UseCaseFactory(IRepositoryFactory repositoryFactory)
			=> _repositoryFactory = repositoryFactory;

		public BaixaEstoque CriarBaixaEstoque()
			=> new(_repositoryFactory);

		public ConsultaEstoque CriarConsultaEstoque()
			=> new(_repositoryFactory);

		public AdicionaEstoque CriarAdicionaEstoque()
			=> new(_repositoryFactory);
	}
}
