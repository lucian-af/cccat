using Cccat.Checkout.Application.Models;

namespace Cccat.Checkout.Application.Gateways
{
	public interface IEstoqueGateway
	{
		public Task BaixarEstoque(BaixaEstoqueDto input);
	}
}
