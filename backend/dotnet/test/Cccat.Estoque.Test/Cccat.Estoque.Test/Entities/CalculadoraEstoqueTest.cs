using Cccat.Estoque.Domain.Entities;
using Cccat.Estoque.Domain.Enums;
using Cccat.Estoque.Domain.Services;

namespace Cccat.Estoque.Tests.Entities;
public class CalculadoraEstoqueTest
{
	[Trait("Cccat", "Domain.Services.Estoque.CalculadoraEstoque")]
	[Fact]
	public void DeveCalcularFluxoEstoque()
	{
		var dados = new List<FluxoEstoque>
		{
			new FluxoEstoque(1,TipoOperacao.Entrada, 10),
			new FluxoEstoque(1,TipoOperacao.Saida, 3),
			new FluxoEstoque(1,TipoOperacao.Entrada, 4),
		};

		var total = CalculadoraEstoque.Calcular(dados);

		Assert.Equal(11, total);
	}
}
