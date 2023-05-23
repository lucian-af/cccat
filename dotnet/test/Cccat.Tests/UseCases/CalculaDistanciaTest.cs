using Cccat.Tests.Fixtures;
using Cccat.UseCases;

namespace Cccat.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class CalculaDistanciaTest
    {
        private readonly CalculaDistanciaFixture _calculaDistanciaFixture;
        private readonly CalculaDistancia _calculaDistancia;

        public CalculaDistanciaTest(DatabaseFixture dbFixture)
        {
            _calculaDistanciaFixture = new(dbFixture.DbContext);
            var cepRepository = _calculaDistanciaFixture.CriarCepRepository(false);
            _calculaDistancia = new CalculaDistancia(cepRepository);
        }

        [Trait("Cccat", "UseCases.CalculaDistancia")]
        [Fact]
        public void DeveCalcularDistancia()
        {
            var input = _calculaDistanciaFixture.CriarInputValido();

            var output = _calculaDistancia.Calcular(input);

            Assert.Equal(695.97m, output.Valor);
        }
    }
}
