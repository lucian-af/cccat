using Cccat.Entities;

namespace Cccat.Tests.Entities
{
    public class CepTest
    {
        [Trait("Cccat", "Entities.Cep")]
        [Fact]
        public void NaoDeveCriarCepComCoordenadasIguais()
        {
            var result = Assert.Throws<Exception>(() => new Cep("17600090", "A", "ABC", 1, 1));

            Assert.NotNull(result);
            Assert.Equal("Coordenadas inválidas.", result.Message);
        }

        [Trait("Cccat", "Entities.Cep")]
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void NaoDeveCriarCepSemCodigo(string codigo)
        {
            var result = Assert.Throws<Exception>(() => new Cep(codigo, "A", "ABC", 1, 2));

            Assert.NotNull(result);
            Assert.Equal("Código do CEP inválido.", result.Message);
        }
    }
}
