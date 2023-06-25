using Cccat.Estoque.Domain.Entities;
using Cccat.Estoque.Domain.Enums;

namespace Cccat.Estoque.Domain.Services;
public static class CalculadoraEstoque
{
	public static int Calcular(List<FluxoEstoque> fluxoEstoque)
	{
		var total = 0;
		fluxoEstoque.ForEach(e =>
		{
			total = e.Operacao switch
			{
				TipoOperacao.Entrada => total + e.Quantidade,
				TipoOperacao.Saida => total - e.Quantidade,
				_ => 0
			};
		});
		return total;
	}
}
