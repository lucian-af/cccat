using Cccat.Checkout.Application.Gateways;
using Cccat.Checkout.Application.Models;
using Cccat.Checkout.Infra.Factories;
using Cccat.Checkout.Infra.HttpClients;

namespace Cccat.Checkout.Infra.Gateways
{
	public class EstoqueHttpGateway : IEstoqueGateway
	{
		private readonly IEstoqueHttpClient _httpClient;

		public EstoqueHttpGateway(HttpClientFactory factory)
			=> _httpClient = factory.CriarEstoqueHttpClient();

		public Task BaixarEstoque(BaixaEstoqueDto input)
			=> _httpClient.BaixarEstoque(input);
	}
}
