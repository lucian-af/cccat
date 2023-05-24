using Cccat.API.Test.Fixtures;
using Cccat.UseCases.Models;
using System.Net.Http.Json;

namespace Cccat.API.Test
{
    [Collection(nameof(CheckoutFixtureCollection))]
    public class CheckoutTest
    {
        private readonly CheckoutFixture _pedidoFixture;

        public CheckoutTest(CheckoutFixture pedidoFixture)
            => _pedidoFixture = pedidoFixture;

        [Trait("Cccat", "API")]
        [Theory]
        [InlineData("/checkout")]
        public async Task POST_DeveCriarPedidoCom3Itens(string pathUrl)
        {
            var client = _pedidoFixture.Client;
            var payload = _pedidoFixture.CriarInputValidoSomenteItens();

            var response = await client.PostAsJsonAsync(pathUrl, payload);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
            Assert.Equal(6090M, result.Total);
        }

        [Trait("Cccat", "API")]
        [Theory]
        [InlineData("/checkout")]
        public async Task POST_DeveCriarPedidoCom3ItensComCupomDesconto(string pathUrl)
        {
            var client = _pedidoFixture.Client;

            var payload = _pedidoFixture.CriarInputValidoComCupom();

            var response = await client.PostAsJsonAsync(pathUrl, payload);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
            Assert.Equal(4872M, result.Total);
        }

        [Trait("Cccat", "API")]
        [Theory]
        [InlineData("/checkout")]
        public async Task POST_DeveAcrescerValorFreteNoPedido(string pathUrl)
        {
            var client = _pedidoFixture.Client;

            var payload = _pedidoFixture.CriarInputValidoComFrete();
            payload.Items.RemoveAll(item => item.IdProduto == 3);

            var response = await client.PostAsJsonAsync(pathUrl, payload);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
            Assert.Equal(6000M, result.SubTotal);
            Assert.Equal(250M, result.Frete);
            Assert.Equal(6250M, result.Total);
        }

        [Trait("Cccat", "API")]
        [Theory]
        [InlineData("/checkout")]
        public async Task POST_DeveAcrescerValorFreteMinimoNoPedido(string pathUrl)
        {
            var client = _pedidoFixture.Client;

            var payload = _pedidoFixture.CriarInputValidoComFrete();

            var response = await client.PostAsJsonAsync(pathUrl, payload);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
            Assert.Equal(6090M, result.SubTotal);
            Assert.Equal(280M, result.Frete);
            Assert.Equal(6370M, result.Total);
        }

        [Trait("Cccat", "API")]
        [Theory]
        [InlineData("/checkout")]
        public async Task POST_NaoDeveAplicarDescontoSeCupomExpirado(string pathUrl)
        {
            var client = _pedidoFixture.Client;

            var payload = _pedidoFixture.CriarInputValidoSomenteItens();
            payload.Cupom = "VALE50";

            var response = await client.PostAsJsonAsync(pathUrl, payload);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
            Assert.Equal(6090M, result.Total);
        }

        [Trait("Cccat", "API")]
        [Theory]
        [InlineData("/checkout")]
        public async Task POST_NaoDeveAplicarDescontoSeCupomInexistente(string pathUrl)
        {
            var client = _pedidoFixture.Client;

            var payload = _pedidoFixture.CriarInputValidoSomenteItens();
            payload.Cupom = "VALE100";

            var response = await client.PostAsJsonAsync(pathUrl, payload);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CheckoutOutputDto>();
            Assert.Equal(6090M, result.Total);
        }

        [Trait("Cccat", "API")]
        [Theory]
        [InlineData("/checkout")]
        public async Task POST_NaoDeveCriarPedidoSeCpfInvalido(string pathUrl)
        {
            var client = _pedidoFixture.Client;
            var payload = new CheckoutInputDto { Cpf = "406.302.170-27" };

            var response = await client.PostAsJsonAsync(pathUrl, payload);

            Assert.False(response.IsSuccessStatusCode);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("Cpf inválido.", result);
        }

        [Trait("Cccat", "API")]
        [Theory]
        [InlineData("/checkout")]
        public async Task POST_NaoDevePermitirItemComQuantidadeNegativa(string pathUrl)
        {
            var client = _pedidoFixture.Client;

            var payload = _pedidoFixture.CriarInputValidoSomenteItens();
            payload.Items.Clear();
            payload.Items.Add(new CheckoutItemDto { IdProduto = 3, Quantidade = -3 });

            var response = await client.PostAsJsonAsync(pathUrl, payload);

            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("Quantidade inválida.", result);
            Assert.False(response.IsSuccessStatusCode);
        }

        [Trait("Cccat", "API")]
        [Theory]
        [InlineData("/checkout")]
        public async Task POST_NaoDevePermitirAdicionarItemDuplicado(string pathUrl)
        {
            var client = _pedidoFixture.Client;

            var payload = _pedidoFixture.CriarInputValidoSomenteItens();
            payload.Items.Add(new CheckoutItemDto { IdProduto = 3, Quantidade = 1 });

            var response = await client.PostAsJsonAsync(pathUrl, payload);

            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("Não é permitido duplicar o mesmo item.", result);
            Assert.False(response.IsSuccessStatusCode);
        }
    }
}
