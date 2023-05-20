using Cccat.Entities;

namespace Cccat.Tests.Entities
{
    public class ProdutoTest
    {
        [Trait("Cccat", "Entities.Produto")]
        [Fact]
        public void DeveCalcularVolume()
        {
            var produto = new Produto(1, "A", 1000, 100, 30, 10, 3);

            Assert.Equal(0.03M, produto.Volume());
        }

        [Trait("Cccat", "Entities.Produto")]
        [Fact]
        public void DeveCalcularDensidade()
        {
            var produto = new Produto(1, "A", 1000, 100, 30, 10, 3);

            Assert.Equal(100, produto.Densidade());
        }

        [Trait("Cccat", "Entities.Produto")]
        [Fact]
        public void NaoDeveCriarProdutoComLarguraInvalida()
        {
            var result = Assert.Throws<Exception>(() => new Produto(1, "A", 1000, -100, 30, 10, 3));

            Assert.NotNull(result);
            Assert.Equal("Dimensões do produto inválidas.", result.Message);
        }

        [Trait("Cccat", "Entities.Produto")]
        [Fact]
        public void NaoDeveCriarProdutoComAlturaInvalida()
        {
            var result = Assert.Throws<Exception>(() => new Produto(1, "A", 1000, 100, -30, 10, 3));

            Assert.NotNull(result);
            Assert.Equal("Dimensões do produto inválidas.", result.Message);
        }

        [Trait("Cccat", "Entities.Produto")]
        [Fact]
        public void NaoDeveCriarProdutoComProfundidadeInvalida()
        {
            var result = Assert.Throws<Exception>(() => new Produto(1, "A", 1000, 100, 30, -10, 3));

            Assert.NotNull(result);
            Assert.Equal("Dimensões do produto inválidas.", result.Message);
        }

        [Trait("Cccat", "Entities.Produto")]
        [Fact]
        public void NaoDeveCriarProdutoComPesoInvalido()
        {
            var result = Assert.Throws<Exception>(() => new Produto(1, "A", 1000, 100, 30, 10, -3));

            Assert.NotNull(result);
            Assert.Equal("Peso do produto inválido.", result.Message);
        }
    }
}
