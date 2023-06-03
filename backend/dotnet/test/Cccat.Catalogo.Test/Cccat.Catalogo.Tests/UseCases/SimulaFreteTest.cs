using Cccat.Catalogo.Application.Models;
using Cccat.Catalogo.Application.UseCase;
using Cccat.Catalogo.Tests.Fixtures;

namespace Cccat.Catalogo.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class SimulaFreteTest
    {
        private readonly SimulaFrete _simulaFrete;

        public SimulaFreteTest(DatabaseFixture dbFixture)
        {
            var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
                .CriarRepositoryFactory();
            _simulaFrete = new SimulaFrete(factory);
        }

        [Trait("Cccat", "UseCases.Catalogo.SimulaFrete")]
        [Fact]
        public void DeveSimularFrete()
        {
            var input = new SimulaFreteInputDto
            {
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                },
                CepOrigem = "17600090",
                CepDestino = "17602700"
            };

            var output = _simulaFrete.Simular(input);

            Assert.Equal(280, output.Frete);
        }
    }
}
