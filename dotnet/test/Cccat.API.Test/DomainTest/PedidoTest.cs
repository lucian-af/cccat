using System.Dynamic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Cccat.API.Test.DomainTest
{
    public class PedidoTest : IClassFixture<CustomWebApiFactory<Program>>
    {
        private readonly CustomWebApiFactory<Program> _factory;

        public PedidoTest(CustomWebApiFactory<Program> factory)
            => _factory = factory;

        [Theory]
        [InlineData("/checkout")]
        public async Task POST_NaoDeveCriarPedidoSeCpfInvalido(string pathUrl)
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsJsonAsync(pathUrl, new { cpf = "406.302.170-27" });

            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("CPF Inválido.", result);
        }

        [Theory]
        [InlineData("/checkout")]
        public async Task POST_DeveCriarPedidoCom3Itens(string pathUrl)
        {
            var client = _factory.CreateClient();

            var pedido = new
            {
                cpf = "407.302.170-27",
                itens = new List<dynamic>
                {
                    new { idProduto = 1, quantidade = 1 },
                    new { idProduto = 2, quantidade = 1 },
                    new { idProduto = 3, quantidade = 3 }
                },
                cupom = ""
            };

            var response = await client.PostAsJsonAsync(pathUrl, pedido);

            var result = await response.Content.ReadFromJsonAsync<dynamic>();
            var pedidoCriado = JsonSerializer.Deserialize<ExpandoObject>(result);
            Assert.Equal(6270M, decimal.Parse(pedidoCriado.total.ToString()));
        }

        [Theory]
        [InlineData("/checkout")]
        public async Task POST_DeveCriarPedidoCom3ItensComCupomDesconto(string pathUrl)
        {
            var client = _factory.CreateClient();

            var pedido = new
            {
                cpf = "407.302.170-27",
                itens = new List<dynamic>
                {
                    new { idProduto = 1, quantidade = 1 },
                    new { idProduto = 2, quantidade = 1 },
                    new { idProduto = 3, quantidade = 3 }
                },
                cupom = "VALE20"
            };

            var response = await client.PostAsJsonAsync(pathUrl, pedido);

            var result = await response.Content.ReadAsStringAsync();
            var pedidoCriado = JsonSerializer.Deserialize<JsonObject>(result);
            var total = (decimal)pedidoCriado["total"];
            Assert.Equal(5016M, total);
        }

        [Theory]
        [InlineData("/checkout")]
        public async Task POST_DeveCriarPedidoCom3ItensSemDescontoSeCupomExpirado(string pathUrl)
        {
            var client = _factory.CreateClient();

            var pedido = new
            {
                cpf = "407.302.170-27",
                itens = new List<dynamic>
                {
                    new { idProduto = 1, quantidade = 1 },
                    new { idProduto = 2, quantidade = 1 },
                    new { idProduto = 3, quantidade = 3 }
                },
                cupom = "VALE10"
            };

            var response = await client.PostAsJsonAsync(pathUrl, pedido);

            var result = await response.Content.ReadAsStringAsync();
            var pedidoCriado = JsonSerializer.Deserialize<JsonObject>(result);
            var total = (decimal)pedidoCriado["total"];
            Assert.Equal(6270M, total);
        }
    }
}
