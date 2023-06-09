﻿using Cccat.Checkout.Application.Gateways;
using Cccat.Checkout.Application.Models;
using Cccat.Checkout.Infra.Factories;
using Cccat.Checkout.Infra.HttpClients;

namespace Cccat.Checkout.Infra.Gateways
{
	public class FreteHttpGateway : IFreteGateway
	{
		private readonly IFreteHttpClient _httpClient;

		public FreteHttpGateway(HttpClientFactory factory)
			=> _httpClient = factory.CriarFreteHttpClient();

		public async Task<FreteDto> Simularfrete(SimulaFreteDto input)
		{
			var response = await _httpClient.SimularFrete(input);
			return new FreteDto
			{
				Frete = response.Frete
			};
		}
	}
}
