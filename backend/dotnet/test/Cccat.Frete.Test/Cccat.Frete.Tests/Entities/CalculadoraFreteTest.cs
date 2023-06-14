using Cccat.Frete.Domain.Services;

namespace Cccat.Frete.Tests.Entities
{
	public class CalculadoraFreteTest
    {
        [Trait("Cccat", "Entities.Frete.CalculadoraFrete")]
        [Fact]
        public void DeveCalcularFrete()
        {
            var frete = CalculadoraFrete.Calcular(1000, 0.03m, 100);
            Assert.Equal(30, frete);
        }

        [Trait("Cccat", "Entities.Frete.CalculadoraFrete")]
        [Fact]
        public void DeveCalcularFreteMinimo()
        {
            var frete = CalculadoraFrete.Calcular(1000, 0.01m, 100);
            Assert.Equal(10, frete);
        }
    }
}
