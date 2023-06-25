using Cccat.Estoque.Application.Models;
using Cccat.Estoque.Application.UseCases;
using Cccat.Estoque.Tests.Fixtures;

namespace Cccat.Estoque.Tests.UseCases
{
	[Collection(nameof(DatabaseFixtureCollection))]
	public class AdicionaEstoqueTest
	{
		private readonly AdicionaEstoque _adicionaEstoque;
		private readonly ConsultaEstoque _consultaEstoque;
		private readonly DatabaseFixture _dbFixture;

		public AdicionaEstoqueTest(DatabaseFixture dbFixture)
		{
			_dbFixture = dbFixture;
			var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
				.CriarRepositoryFactory();

			_adicionaEstoque = new(factory);
			_consultaEstoque = new(factory);
		}

		[Trait("Cccat", "UseCases.Estoque.AdicionaEstoque")]
		[Fact]
		public async Task DeveAdicionarEstoqueDosProdutos()
		{
			await _dbFixture.LimparBase();

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

			await _adicionaEstoque.Adicionar(input);

			var output1 = await _consultaEstoque.Consultar(1);
			var output2 = await _consultaEstoque.Consultar(2);
			var output3 = await _consultaEstoque.Consultar(3);

			Assert.Equal(10, output1.Total);
			Assert.Equal(5, output2.Total);
			Assert.Equal(9, output3.Total);
		}
	}
}
