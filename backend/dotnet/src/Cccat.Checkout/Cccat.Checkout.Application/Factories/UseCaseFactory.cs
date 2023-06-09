using Cccat.Checkout.Domain.Interfaces;
using US = Cccat.Checkout.Application.UseCase;

namespace Cccat.Checkout.Application.Factories
{
    public class UseCaseFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IGatewayFactory _gatewayFactory;

        public UseCaseFactory(IRepositoryFactory repositoryFactory, IGatewayFactory gatewayFactory)
        {
            _repositoryFactory = repositoryFactory;
            _gatewayFactory = gatewayFactory;
        }

        public US.Checkout CriarCheckout() => new(_repositoryFactory, _gatewayFactory);
    }
}
