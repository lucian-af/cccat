using Cccat.Catalogo.Domain.Entities;

namespace Cccat.Catalogo.Domain.Interfaces
{
    public interface ICupomRepository
    {
        public Cupom Get(string codigo);
    }
}
