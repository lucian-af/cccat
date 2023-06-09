using System.Net.Http.Json;
using Cccat.Checkout.API.Test.Fixtures;
using Cccat.Checkout.Application.Models;

namespace Cccat.Checkout.API.Test.Controllers
{
	[Collection(nameof(WebApiFixtureCollection))]
	public class CheckoutTest : IClassFixture<CheckoutFixture>
	{
		private readonly CheckoutFixture _checkoutFixture;
		private readonly HttpClient _httpClient;

		public CheckoutTest(CheckoutFixture checkoutFixture, WebApiFixture webApiFixture)
		{
			_checkoutFixture = checkoutFixture;
			_httpClient = webApiFixture.Client;
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task POST_DeveCriarPedidoCom3Itens(string pathUrl)
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
			Assert.Equal(6090M, result.Total);
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task POST_DeveCriarPedidoCom3ItensComCupomDesconto(string pathUrl)
		{
			var payload = _checkoutFixture.CriarInputValidoComCupom();

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
			Assert.Equal(4872M, result.Total);
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task POST_DeveAcrescerValorFreteNoPedido(string pathUrl)
		{
			var payload = _checkoutFixture.CriarInputValidoComFrete();
			payload.Items.RemoveAll(item => item.IdProduto == 3);

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
			Assert.Equal(6000M, result.SubTotal);
			Assert.Equal(250M, result.Frete);
			Assert.Equal(6250M, result.Total);
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task POST_DeveAcrescerValorFreteMinimoNoPedido(string pathUrl)
		{
			var payload = _checkoutFixture.CriarInputValidoComFrete();

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
			Assert.Equal(6090M, result.SubTotal);
			Assert.Equal(280M, result.Frete);
			Assert.Equal(6370M, result.Total);
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task POST_NaoDeveAplicarDescontoSeCupomExpirado(string pathUrl)
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();
			payload.Cupom = "VALE50";

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
			Assert.Equal(6090M, result.Total);
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task POST_NaoDeveAplicarDescontoSeCupomInexistente(string pathUrl)
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();
			payload.Cupom = "VALE100";

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
			Assert.Equal(6090M, result.Total);
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task POST_NaoDeveCriarPedidoSeCpfInvalido(string pathUrl)
		{
			var payload = new CheckoutInputDto { Cpf = "406.302.170-27" };

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			Assert.False(response.IsSuccessStatusCode);
			var result = await response.Content.ReadAsStringAsync();
			Assert.Equal("Cpf inválido.", result);
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task POST_NaoDevePermitirItemComQuantidadeNegativa(string pathUrl)
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();
			payload.Items.Clear();
			payload.Items.Add(new CheckoutItemDto { IdProduto = 3, Quantidade = -3 });

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			var result = await response.Content.ReadAsStringAsync();
			Assert.Equal("Quantidade inválida.", result);
			Assert.False(response.IsSuccessStatusCode);
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task POST_NaoDevePermitirAdicionarItemDuplicado(string pathUrl)
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();
			payload.Items.Add(new CheckoutItemDto { IdProduto = 3, Quantidade = 1 });

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			var result = await response.Content.ReadAsStringAsync();
			Assert.Equal("Não é permitido duplicar o mesmo item.", result);
			Assert.False(response.IsSuccessStatusCode);
		}

		[Trait("Cccat", "API.Checkout")]
		[Theory]
		[InlineData("/api/checkout")]
		public async Task DeveCriarPedidoCom3ItensComCupomEFrete(string pathUrl)
		{
			var payload = _checkoutFixture.CriarInputValidoComCupomEFrete();

			var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
			Assert.Equal(6090M, result.SubTotal);
			Assert.Equal(5152M, result.Total);
			Assert.Equal(280M, result.Frete);
			Assert.Equal(1218M, result.Desconto);
		}
	}
}
