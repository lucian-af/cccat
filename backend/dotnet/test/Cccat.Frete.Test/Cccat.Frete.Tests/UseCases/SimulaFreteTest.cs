using Cccat.Frete.Application.Models;
using Cccat.Frete.Application.UseCase;
using Cccat.Frete.Tests.Fixtures;

namespace Cccat.Frete.Tests.UseCases
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

        [Trait("Cccat", "UseCases.Frete.SimulaFrete")]
        [Fact]
        public void DeveSimularFrete()
        {
            var input = new SimulaFreteInputDto
            {
                Items = new()
                {
                    new SimulaFreteItemDto
                    {
                        Volume = 0.3m,
                        Densidade = 100,
                        Quantidade = 3
                    }
                },
                CepOrigem = "17600090",
                CepDestino = "17602700"
            };

            var output = _simulaFrete.Simular(input);

            Assert.Equal(900, output.Frete);
        }
    }
}
