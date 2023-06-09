using Cccat.Checkout.Application.Gateways;

namespace Cccat.Checkout.Application.Factories
{
    public interface IGatewayFactory
    {
        public ICatalogoGateway CriarCatalogoGateway();
        public IFreteGateway CriarFreteGateway();
    }
}
