using Cccat.Frete.Application.UseCase;
using Cccat.Frete.Domain.Interfaces;

namespace Cccat.Frete.Application.Factories
{
    public class UseCaseFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UseCaseFactory(IRepositoryFactory repositoryFactory)
            => _repositoryFactory = repositoryFactory;

        public SimulaFrete CriarSimulaFrete() => new(_repositoryFactory);
    }
}
