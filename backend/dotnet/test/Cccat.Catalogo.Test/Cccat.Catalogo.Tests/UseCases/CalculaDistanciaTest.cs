using Cccat.Application.Models;
using Cccat.Application.UseCase;
using Cccat.Tests.Fixtures;

namespace Cccat.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class CalculaDistanciaTest
    {
        private readonly CalculaDistancia _calculaDistancia;

        public CalculaDistanciaTest(DatabaseFixture dbFixture)
        {
            var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
                .CriarRepositoryFactory();
            _calculaDistancia = new CalculaDistancia(factory);
        }

        [Trait("Cccat", "UseCases.CalculaDistancia")]
        [Fact]
        public void DeveCalcularDistancia()
        {
            var input = new CalculaDistanciaInputDto
            {
                CepOrigem = "17600090",
                CepDestino = "72980000"
            };

            var output = _calculaDistancia.Calcular(input);

            Assert.Equal(695.97m, output.Valor);
        }
    }
}
