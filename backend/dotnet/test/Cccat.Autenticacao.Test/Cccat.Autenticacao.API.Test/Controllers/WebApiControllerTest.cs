using System.Net.Http.Json;
using Cccat.Autenticacao.API.Test.Fixtures;
using Cccat.Autenticacao.Application.Models;

namespace Cccat.Autenticacao.API.Test.Controllers
{
	[Collection(nameof(WebApiFixtureCollection))]
	public class WebApiControllerTest
	{
		private readonly HttpClient _httpClient;

		public WebApiControllerTest(WebApiFixture webApiFixture)
			=> _httpClient = webApiFixture.Client;

		[Trait("Cccat", "API.Autenticacao")]
		[Theory]
		[InlineData("api/autenticacao/cadastrar")]
		public async Task GET_DeveCadastrarUsuario(string pathUrl)
		{
			var request = new CadastraUsuarioInputDto
			{
				Email = "lucian@teste.com",
				Senha = "123aBc"
			};

			var response = await _httpClient.PostAsJsonAsync(pathUrl, request);

			Assert.True(response.IsSuccessStatusCode);

			var idUsuario = await response.Content.ReadFromJsonAsync<string>();
			Assert.NotNull(idUsuario);
			Assert.True(Guid.TryParse(idUsuario, out var _));
		}
	}
}
