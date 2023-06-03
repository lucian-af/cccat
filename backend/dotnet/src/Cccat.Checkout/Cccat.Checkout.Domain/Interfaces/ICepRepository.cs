using Cccat.Checkout.Domain.Entities;

namespace Cccat.Checkout.Domain.Interfaces
{
    public interface ICepRepository
    {
        public Cep Get(string codigo);

        public void AdicionarCep(Cep cep);
    }
}
