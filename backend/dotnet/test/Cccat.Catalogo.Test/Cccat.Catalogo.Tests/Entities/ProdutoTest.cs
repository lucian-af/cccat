using Cccat.Catalogo.Domain.Entities;

namespace Cccat.Catalogo.Tests.Entities
{
	public class ProdutoTest
	{
		[Trait("Cccat", "Entities.Catalogo.Produto")]
		[Fact]
		public void DeveCalcularDensidade()
		{
			var produto = new Produto(1, "A", 1000, 100, 30, 10, 3);

			Assert.Equal(100, produto.Densidade());
		}

		[Trait("Cccat", "Entities.Catalogo.Produto")]
		[Fact]
		public void NaoDeveCriarProdutoComPesoInvalido()
		{
			var result = Assert.Throws<Exception>(() => new Produto(1, "A", 1000, 100, 30, 10, -3));

			Assert.NotNull(result);
			Assert.Equal("Peso do produto inválido.", result.Message);
		}
	}
}
