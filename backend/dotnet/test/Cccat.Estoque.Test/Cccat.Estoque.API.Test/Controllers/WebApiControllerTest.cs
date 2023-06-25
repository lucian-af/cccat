using System.Net.Http.Json;
using Cccat.Estoque.API.Test.Fixtures;
using Cccat.Estoque.Application.Models;

namespace Cccat.Estoque.API.Test.Controllers
{
	[Collection(nameof(WebApiFixtureCollection))]
	public class WebApiControllerTest
	{
		private readonly HttpClient _httpClient;
		private readonly WebApiFixture _webApiFixture;

		public WebApiControllerTest(WebApiFixture webApiFixture)
		{
			_httpClient = webApiFixture.Client;
			_webApiFixture = webApiFixture;
		}

		[Trait("Cccat", "API.Estoque")]
		[Fact]
		public async Task POST_DeveBaixarEstoqueDosProdutos()
		{
			await _webApiFixture.LimparBase();
			var input = new BaixaEstoqueInputDto
			{
				Itens = new List<BaixaEstoqueItemInputDto>
				{
					new BaixaEstoqueItemInputDto
					{
						IdProduto = 1,
						Quantidade = 10
					},
					new BaixaEstoqueItemInputDto
					{
						IdProduto = 2,
						Quantidade = 5
					},
					new BaixaEstoqueItemInputDto
					{
						IdProduto = 3,
						Quantidade = 9
					}
				}
			};

			var response = await _httpClient.PostAsJsonAsync("api/baixar-estoque", input);

			response.EnsureSuccessStatusCode();

			var outputResponseProduto1 = await _httpClient.GetAsync($"api/consultar-estoque/1");
			outputResponseProduto1.EnsureSuccessStatusCode();
			var outputProduto1 = await outputResponseProduto1.Content.ReadFromJsonAsync<ConsultaEstoqueOutputDto>();

			var outputResponseProduto2 = await _httpClient.GetAsync($"api/consultar-estoque/2");
			outputResponseProduto2.EnsureSuccessStatusCode();
			var outputProduto2 = await outputResponseProduto2.Content.ReadFromJsonAsync<ConsultaEstoqueOutputDto>();

			var outputResponseProduto3 = await _httpClient.GetAsync($"api/consultar-estoque/3");
			outputResponseProduto3.EnsureSuccessStatusCode();
			var outputProduto3 = await outputResponseProduto3.Content.ReadFromJsonAsync<ConsultaEstoqueOutputDto>();

			Assert.Equal(-10, outputProduto1.Total);
			Assert.Equal(-5, outputProduto2.Total);
			Assert.Equal(-9, outputProduto3.Total);
		}

		[Trait("Cccat", "API.Estoque")]
		[Fact]
		public async Task POST_DeveAdicionarEstoqueDeProdutos()
		{
			await _webApiFixture.LimparBase();
			var input = new AdicionaEstoqueInputDto
			{
				Itens = new List<AdicionaEstoqueItemInputDto>
				{
					new AdicionaEstoqueItemInputDto
					{
						IdProduto = 1,
						Quantidade = 10
					},
					new AdicionaEstoqueItemInputDto
					{
						IdProduto = 2,
						Quantidade = 5
					},
					new AdicionaEstoqueItemInputDto
					{
						IdProduto = 3,
						Quantidade = 9
					}
				}
			};

			var response = await _httpClient.PostAsJsonAsync("api/adicionar-estoque", input);

			response.EnsureSuccessStatusCode();

			var outputResponseProduto1 = await _httpClient.GetAsync($"api/consultar-estoque/1");
			outputResponseProduto1.EnsureSuccessStatusCode();
			var outputProduto1 = await outputResponseProduto1.Content.ReadFromJsonAsync<ConsultaEstoqueOutputDto>();

			var outputResponseProduto2 = await _httpClient.GetAsync($"api/consultar-estoque/2");
			outputResponseProduto2.EnsureSuccessStatusCode();
			var outputProduto2 = await outputResponseProduto2.Content.ReadFromJsonAsync<ConsultaEstoqueOutputDto>();

			var outputResponseProduto3 = await _httpClient.GetAsync($"api/consultar-estoque/3");
			outputResponseProduto3.EnsureSuccessStatusCode();
			var outputProduto3 = await outputResponseProduto3.Content.ReadFromJsonAsync<ConsultaEstoqueOutputDto>();

			Assert.Equal(10, outputProduto1.Total);
			Assert.Equal(5, outputProduto2.Total);
			Assert.Equal(9, outputProduto3.Total);
		}
	}
}
