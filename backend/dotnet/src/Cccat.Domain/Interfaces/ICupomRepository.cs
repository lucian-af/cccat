using Cccat.Domain.Entities;

namespace Cccat.Domain.Interfaces
{
    public interface ICupomRepository
    {
        public Cupom Get(string codigo);
    }
}
