using Cccat.Checkout.Domain.Interfaces;
using US = Cccat.Checkout.Application.UseCase;

namespace Cccat.Checkout.Application.Factories
{
    public class UseCaseFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UseCaseFactory(IRepositoryFactory repositoryFactory)
            => _repositoryFactory = repositoryFactory;

        public US.Checkout CriarCheckout() => new(_repositoryFactory);

        public US.ConsultaProduto CriarConsultaProduto() => new(_repositoryFactory);
    }
}
