using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.Test.ObjetoEstudoRefatorado
{
    public class SegmentoTest
    {
        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveRetornarExceptionSeDataInvalida()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                new Segmento(10, DateTime.MinValue);
            });

            Assert.Equal("Data hora inválida.", exception.Message);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveRetornarExceptionSeDistanciaInvalida()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                new Segmento(-2, new DateTime(2023, 04, 11, 2, 0, 0));
            });

            Assert.Equal("Distância inválida.", exception.Message);
        }
    }
}