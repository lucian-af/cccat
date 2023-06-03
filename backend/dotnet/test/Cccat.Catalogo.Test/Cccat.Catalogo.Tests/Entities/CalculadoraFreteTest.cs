using Cccat.Catalogo.Domain.Entities;

namespace Cccat.Catalogo.Tests.Entities
{
    public class CalculadoraFreteTest
    {
        [Trait("Cccat", "Entities.CalculadoraFrete")]
        [Fact]
        public void DeveCalcularFrete()
        {
            var produto = new Produto(1, "A", 1000, 1, 1, 1, 3);

            var frete = CalculadoraFrete.Calcular(produto);

            Assert.Equal(30, frete);
        }

        [Trait("Cccat", "Entities.CalculadoraFrete")]
        [Fact]
        public void DeveCalcularFreteMinimo()
        {
            var produto = new Produto(1, "A", 1000, 10, 10, 10, 0.9M);

            var frete = CalculadoraFrete.Calcular(produto);

            Assert.Equal(10, frete);
        }
    }
}
