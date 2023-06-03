using Cccat.Checkout.API.Test.Fixtures;
using Cccat.Checkout.Application.Models;
using System.Net.Http.Json;

namespace Cccat.Checkout.API.Test.Controllers
{
    [Collection(nameof(WebApiFixtureCollection))]
    public class ConsultaProdutoTest
    {
        private readonly HttpClient _httpClient;

        public ConsultaProdutoTest(WebApiFixture webApiFixture)
            => _httpClient = webApiFixture.Client;

        [Trait("Cccat", "API.Checkout")]
        [Theory]
        [InlineData("api/produtos")]
        public async Task GET_DeveConsultarTodosProdutos(string pathUrl)
        {
            var response = await _httpClient.GetAsync(pathUrl);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<ConsultaProdutoOutputDto>>();

            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }
    }
}
