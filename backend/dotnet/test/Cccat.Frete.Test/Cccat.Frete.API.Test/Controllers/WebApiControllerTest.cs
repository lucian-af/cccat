using Cccat.Frete.API.Test.Fixtures;
using Cccat.Frete.Application.Models;
using System.Net.Http.Json;

namespace Cccat.Frete.API.Test.Controllers
{
    [Collection(nameof(WebApiFixtureCollection))]
    public class WebApiControllerTest
    {
        private readonly HttpClient _httpClient;

        public WebApiControllerTest(WebApiFixture webApiFixture)
        {
            _httpClient = webApiFixture.Client;
        }

        [Trait("Cccat", "API.Frete")]
        [Theory]
        [InlineData("/api/simularfrete")]
        public async Task POST_DeveCalcularFrete(string pathUrl)
        {
            var payload = new SimulaFreteInputDto
            {
                CepOrigem = "17600090",
                CepDestino = "72980000",
                Items = new()
                {
                    new SimulaFreteItemDto
                    {
                        Volume = 0.3m,
                        Densidade = 100,
                        Quantidade = 3
                    }
                }
            };
            var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<SimulaFreteOutputDto>();
            Assert.Equal(900, result.Frete);
        }

        [Trait("Cccat", "API.Frete")]
        [Theory]
        [InlineData("/api/simularfrete")]
        public async Task POST_DeveCalcularFreteMinimo(string pathUrl)
        {
            var payload = new SimulaFreteInputDto
            {
                CepOrigem = "17600090",
                CepDestino = "72980000",
                Items = new()
                {
                    new SimulaFreteItemDto
                    {
                        Volume = 0.1m,
                        Densidade = 10,
                        Quantidade = 1
                    }
                }
            };
            var response = await _httpClient.PostAsJsonAsync(pathUrl, payload);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<SimulaFreteOutputDto>();
            Assert.Equal(10, result.Frete);
        }
    }
}
