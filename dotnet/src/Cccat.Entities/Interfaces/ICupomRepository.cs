using Cccat.Entities.Negocio;

namespace Cccat.Entities.Interfaces
{
    public interface ICupomRepository
    {
        public Cupom Get(string codigo);
    }
}
