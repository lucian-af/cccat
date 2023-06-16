using System.Net.Http.Json;
using Cccat.Autenticacao.API.Test.Fixtures;
using Cccat.Autenticacao.Application.Models;
using Cccat.Autenticacao.Domain.Services;

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
			var cadastraUsuarioInput = new CadastraUsuarioInputDto
			{
				Email = "lucian@teste.com",
				Senha = "1q2w3e4r"
			};

			var response = await _httpClient.PostAsJsonAsync(pathUrl, cadastraUsuarioInput);

			Assert.True(response.IsSuccessStatusCode);

			var idUsuario = await response.Content.ReadFromJsonAsync<string>();
			Assert.NotNull(idUsuario);
			Assert.True(Guid.TryParse(idUsuario, out var _));
		}

		[Trait("Cccat", "API.Autenticacao")]
		[Fact]
		public async Task GET_DeveAutenticarUsuario()
		{
			var cadastraUsuarioInput = new CadastraUsuarioInputDto
			{
				Email = "cccat@teste.com",
				Senha = "123aBc"
			};

			await _httpClient.PostAsJsonAsync("api/autenticacao/cadastrar", cadastraUsuarioInput);
			var autenticaUsuarioInput = new AutenticaUsuarioInputDto
			{
				Email = cadastraUsuarioInput.Email,
				Senha = cadastraUsuarioInput.Senha
			};

			var response = await _httpClient.PostAsJsonAsync("api/autenticacao/autenticar", autenticaUsuarioInput);

			Assert.True(response.IsSuccessStatusCode);
			var output = await response.Content.ReadFromJsonAsync<AutenticaUsuarioOutputDto>();
			Assert.NotNull(output);
			Assert.True(TokenJwt.Validar(output.Token));
		}
	}
}
