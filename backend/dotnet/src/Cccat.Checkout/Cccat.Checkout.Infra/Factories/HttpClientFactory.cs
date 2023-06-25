using Cccat.Checkout.Infra.HttpClients;
using Microsoft.Extensions.DependencyInjection;

namespace Cccat.Checkout.Infra.Factories
{
	public class HttpClientFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public HttpClientFactory(IServiceProvider serviceProvider)
		{
			if (serviceProvider is null)
				ArgumentException.ThrowIfNullOrEmpty(nameof(serviceProvider));

			_serviceProvider = serviceProvider;
		}

		public IEstoqueHttpClient CriarEstoqueHttpClient()
			=> _serviceProvider.GetRequiredService<IEstoqueHttpClient>();

		public ICatalogoHttpClient CriarCatalogoHttpClient()
			=> _serviceProvider.GetRequiredService<ICatalogoHttpClient>();

		public IFreteHttpClient CriarFreteHttpClient()
			=> _serviceProvider.GetRequiredService<IFreteHttpClient>();

		public IAutenticacaoHttpClient CriarAutenticacaoHttpClient()
			=> _serviceProvider.GetRequiredService<IAutenticacaoHttpClient>();
	}
}
