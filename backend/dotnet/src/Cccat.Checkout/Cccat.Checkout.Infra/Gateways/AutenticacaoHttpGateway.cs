using Cccat.Checkout.Infra.Factories;
using Cccat.Checkout.Infra.HttpClients;
using Cccat.Checkout.Infra.HttpClients.Dtos;

namespace Cccat.Checkout.Infra.Gateways
{
	public class AutenticacaoHttpGateway
	{
		private readonly IAutenticacaoHttpClient _httpClient;

		public AutenticacaoHttpGateway(HttpClientFactory factory)
			=> _httpClient = factory.CriarAutenticacaoHttpClient();

		public async Task<AutenticacaoResponseDto> Autenticar(AutenticacaoDto dados)
		{
			var response = await _httpClient.Autenticar(dados);
			return response;
		}
	}
}
