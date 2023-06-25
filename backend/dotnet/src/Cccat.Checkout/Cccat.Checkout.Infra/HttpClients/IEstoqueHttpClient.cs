using Cccat.Checkout.Application.Models;
using Refit;

namespace Cccat.Checkout.Infra.HttpClients
{
	public interface IEstoqueHttpClient
	{
		[Post("/baixar-estoque")]
		public Task BaixarEstoque(BaixaEstoqueDto request);
	}
}
