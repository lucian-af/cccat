using Cccat.Checkout.Application.Factories;
using Cccat.Checkout.Application.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace Cccat.Checkout.Infra.Factories
{
    public class GatewayHttpFactory : IGatewayFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public GatewayHttpFactory(IServiceProvider serviceProvider)
        {
            if (serviceProvider is null)
                ArgumentException.ThrowIfNullOrEmpty(nameof(serviceProvider));

            _serviceProvider = serviceProvider;
        }

        public ICatalogoGateway CriarCatalogoGateway()
            => _serviceProvider.GetRequiredService<ICatalogoGateway>();

        public IFreteGateway CriarFreteGateway()
            => _serviceProvider.GetRequiredService<IFreteGateway>();
    }
}
