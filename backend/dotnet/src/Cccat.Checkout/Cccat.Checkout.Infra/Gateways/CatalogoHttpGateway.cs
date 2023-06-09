using Cccat.Checkout.Application.Gateways;
using Cccat.Checkout.Domain.Entities;
using Cccat.Checkout.Infra.HttpClients;

namespace Cccat.Checkout.Infra.Gateways
{
    public class CatalogoHttpGateway : ICatalogoGateway
    {
        private readonly ICatalogoHttpClient _httpClient;

        public CatalogoHttpGateway(ICatalogoHttpClient httpClient)
            => _httpClient = httpClient;

        public async Task<ProdutoDto> ConsultarProduto(int idProduto)
        {
            var response = await _httpClient.ConsultaProduto(idProduto);
            return new()
            {
                Id = response.IdProduto,
                Descricao = response.Descricao,
                Preco = response.Preco,
                Largura = response.Largura,
                Altura = response.Altura,
                Profundidade = response.Profundidade,
                Peso = response.Peso,
                Densidade = response.Densidade,
                Volume = response.Volume
            };
        }
    }
}
