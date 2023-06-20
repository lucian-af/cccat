using Cccat.Estoque.API.Test.Fixtures;
using System.Net.Http.Json;

namespace Cccat.Estoque.API.Test.Controllers
{
	[Collection(nameof(WebApiFixtureCollection))]
	public class WebApiControllerTest
	{
		private readonly HttpClient _httpClient;

		public WebApiControllerTest(WebApiFixture webApiFixture)
			=> _httpClient = webApiFixture.Client;

		[Trait("Contexto", "API")]
		[Theory]
		[InlineData("api/route")]
		public async Task GET(string pathUrl)
		{
			var response = await _httpClient.GetAsync(pathUrl);

			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<List<string>>();

			Assert.NotNull(result);
		}
	}
}
