using Cccat.Catalogo.Domain.Entities;

namespace Cccat.Catalogo.Tests.Entities
{
	public class ProdutoDimensaoTest
	{
		[Trait("Cccat", "Entities.Catalogo.ProdutoDimensao")]
		[Fact]
		public void DeveCalcularVolume()
		{
			var dimensao = new ProdutoDimensao(100, 30, 10);

			Assert.Equal(0.03M, dimensao.Volume());
		}

		[Trait("Cccat", "Entities.Catalogo.ProdutoDimensao")]
		[Fact]
		public void NaoDeveCriarProdutoComLarguraInvalida()
		{
			var result = Assert.Throws<Exception>(() => new ProdutoDimensao(-100, 30, 10));

			Assert.NotNull(result);
			Assert.Equal("Dimensões do produto inválidas.", result.Message);
		}

		[Trait("Cccat", "Entities.Catalogo.ProdutoDimensao")]
		[Fact]
		public void NaoDeveCriarProdutoComAlturaInvalida()
		{
			var result = Assert.Throws<Exception>(() => new ProdutoDimensao(100, -30, 10));

			Assert.NotNull(result);
			Assert.Equal("Dimensões do produto inválidas.", result.Message);
		}

		[Trait("Cccat", "Entities.Catalogo.ProdutoDimensao")]
		[Fact]
		public void NaoDeveCriarProdutoComProfundidadeInvalida()
		{
			var result = Assert.Throws<Exception>(() => new ProdutoDimensao(100, 30, -10));

			Assert.NotNull(result);
			Assert.Equal("Dimensões do produto inválidas.", result.Message);
		}
	}
}
