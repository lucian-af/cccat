using Cccat.Frete.Domain.Entities;

namespace Cccat.Frete.Domain.Interfaces
{
    public interface ICupomRepository
    {
        public Cupom Get(string codigo);
    }
}
