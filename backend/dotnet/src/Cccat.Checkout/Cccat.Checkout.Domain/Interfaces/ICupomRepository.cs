using Cccat.Checkout.Domain.Entities;

namespace Cccat.Checkout.Domain.Interfaces
{
    public interface ICupomRepository
    {
        public Cupom Get(string codigo);
    }
}
