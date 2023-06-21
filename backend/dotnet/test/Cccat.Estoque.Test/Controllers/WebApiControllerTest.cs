using System.Net.Http.Json;
using Cccat.Estoque.API.Test.Fixtures;

namespace Cccat.Estoque.API.Test.Controllers
{
	[Collection(nameof(WebApiFixtureCollection))]
	public class WebApiControllerTest
	{
		private readonly HttpClient _httpClient;

		public WebApiControllerTest(WebApiFixture webApiFixture)
			=> _httpClient = webApiFixture.Client;

		[Trait("Cccat", "API.Estoque")]
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
