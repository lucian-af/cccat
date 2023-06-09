using Cccat.Checkout.Application.Models;

namespace Cccat.Checkout.Application.Gateways
{
    public interface IFreteGateway
    {
        public Task<FreteDto> Simularfrete(SimulaFreteDto input);
    }
}
