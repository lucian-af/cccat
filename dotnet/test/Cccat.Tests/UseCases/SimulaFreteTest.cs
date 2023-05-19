using Cccat.Tests.Fixtures;
using Cccat.UseCases;

namespace Cccat.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class SimulaFreteTest
    {
        private readonly SimulaFreteFixture _simulaFreteFixture;
        private readonly SimulaFrete _simulaFrete;

        public SimulaFreteTest(DatabaseFixture dbFixture)
        {
            _simulaFreteFixture = new(dbFixture.DbContext);
            var produtoRepository = _simulaFreteFixture.CriarProdutoRepository(false);
            _simulaFrete = new SimulaFrete(produtoRepository);
        }

        [Trait("Cccat", "UseCases.SimulaFrete")]
        [Fact]
        public void DeveSimularFrete()
        {
            var input = _simulaFreteFixture.CriarInputValido();

            var output = _simulaFrete.Simular(input);

            Assert.Equal(280, output.Frete);
        }
    }
}
