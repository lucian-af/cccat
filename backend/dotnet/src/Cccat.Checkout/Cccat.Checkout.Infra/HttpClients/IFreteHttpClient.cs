using Cccat.Checkout.Application.Models;
using Refit;

namespace Cccat.Checkout.Infra.HttpClients
{
    public interface IFreteHttpClient
    {
        [Post("/simularfrete")]
        public Task<SimulaFreteResponseDto> SimularFrete(SimulaFreteDto request);
    }
}
