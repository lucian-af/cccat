using Cccat.Checkout.Infra.HttpClients.Dtos;
using Refit;

namespace Cccat.Checkout.Infra.HttpClients
{
    public interface ICatalogoHttpClient
    {
        [Get("/produtos/{idProduto}")]
        public Task<ConsultaProdutoResponseDto> ConsultaProduto(int idProduto);
    }
}
