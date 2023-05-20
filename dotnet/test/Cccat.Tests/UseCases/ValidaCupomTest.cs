using Cccat.Tests.Fixtures;
using Cccat.UseCases;

namespace Cccat.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class ValidaCupomTest
    {
        private readonly ValidaCupomFixture _validaCupomFixture;
        private readonly ValidaCupom _validaCupom;

        public ValidaCupomTest(DatabaseFixture dbFixture)
        {
            _validaCupomFixture = new(dbFixture.DbContext);
            var cupomRepository = _validaCupomFixture.CriarCupomRepository(false);
            _validaCupom = new(cupomRepository);
        }

        [Trait("Cccat", "UseCases.ValidaCupom")]
        [Fact]
        public void DeveValidarSeCupomValido()
        {
            var codigo = "VALE20";

            var output = _validaCupom.EhValido(codigo);

            Assert.True(output);
        }

        [Trait("Cccat", "UseCases.ValidaCupom")]
        [Fact]
        public void DeveValidarSeCupomExpirado()
        {
            var codigo = "VALE50";

            var output = _validaCupom.EhValido(codigo);

            Assert.False(output);
        }
    }
}
