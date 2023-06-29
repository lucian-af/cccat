using Cccat.Checkout.Application.Models;

namespace Cccat.Checkout.Application.Gateways
{
	public interface ICatalogoGateway
    {
        public Task<ProdutoDto> ConsultarProduto(int idProduto);
    }
}
