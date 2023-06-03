using Cccat.Catalogo.API.Test.Fixtures;
using Cccat.Catalogo.Application.Models;
using System.Net.Http.Json;

namespace Cccat.Catalogo.API.Test.Controllers
{
    [Collection(nameof(WebApiFixtureCollection))]
    public class WebApiControllerTest
    {
        private readonly HttpClient _httpClient;

        public WebApiControllerTest(WebApiFixture webApiFixture)
            => _httpClient = webApiFixture.Client;

        [Trait("Cccat", "API.Catalogo")]
        [Theory]
        [InlineData("api/produtos")]
        public async Task GET_DeveConsultarTodosProdutos(string pathUrl)
        {
            var response = await _httpClient.GetAsync(pathUrl);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<ConsultaProdutosOutputDto>>();

            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }

        [Trait("Cccat", "API.Catalogo")]
        [Theory]
        [InlineData("api/produtos/1")]
        public async Task GET_DeveConsultarProdutoPorId(string pathUrl)
        {
            var response = await _httpClient.GetAsync(pathUrl);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ConsultaProdutoOutputDto>();

            Assert.NotNull(result);
            Assert.Equal(1, result.IdProduto);
            Assert.Equal("A", result.Descricao);
            Assert.Equal(1000, result.Preco);
            Assert.Equal(100, result.Largura);
            Assert.Equal(30, result.Altura);
            Assert.Equal(10, result.Profundidade);
            Assert.Equal(3, result.Peso);
            Assert.Equal(0.03m, result.Volume);
            Assert.Equal(100, result.Densidade);
        }
    }
}
