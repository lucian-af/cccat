using Cccat.Checkout.Domain.Entities;

namespace Cccat.Checkout.Application.Gateways
{
    public interface ICatalogoGateway
    {
        public Task<ProdutoDto> ConsultarProduto(int idProduto);
    }
}
