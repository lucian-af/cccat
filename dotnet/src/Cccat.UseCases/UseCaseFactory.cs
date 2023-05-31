using Cccat.Infra.Repositories;

namespace Cccat.UseCases
{
    public class UseCaseFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UseCaseFactory(IRepositoryFactory repositoryFactory)
            => _repositoryFactory = repositoryFactory;

        public Checkout CriarCheckout() => new(_repositoryFactory);

        public ConsultaProduto CriarConsultaProduto() => new(_repositoryFactory);
    }
}
