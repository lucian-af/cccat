using Cccat.Estoque.Application.Models;
using Cccat.Estoque.Application.UseCases;
using Cccat.Estoque.Tests.Fixtures;

namespace Cccat.Estoque.Tests.UseCases
{
	[Collection(nameof(DatabaseFixtureCollection))]
	public class BaixaEstoqueTest
	{
		private readonly BaixaEstoque _baixaEstoque;
		private readonly ConsultaEstoque _consultaEstoque;
		private readonly DatabaseFixture _dbFixture;

		public BaixaEstoqueTest(DatabaseFixture dbFixture)
		{
			_dbFixture = dbFixture;
			var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
				.CriarRepositoryFactory();

			_baixaEstoque = new(factory);
			_consultaEstoque = new(factory);
		}

		[Trait("Cccat", "UseCases.Estoque.BaixaEstoque")]
		[Fact]
		public async Task DeveBaixarEstoqueDosProdutos()
		{
			await _dbFixture.LimparBase();

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

			await _baixaEstoque.Baixar(input);

			var output1 = await _consultaEstoque.Consultar(1);
			var output2 = await _consultaEstoque.Consultar(2);
			var output3 = await _consultaEstoque.Consultar(3);

			Assert.Equal(-10, output1.Total);
			Assert.Equal(-5, output2.Total);
			Assert.Equal(-9, output3.Total);
		}
	}
}
