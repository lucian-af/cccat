using Cccat.Frete.Domain.Entities;

namespace Cccat.Frete.Domain.Interfaces
{
    public interface ICepRepository
    {
        public Cep Get(string codigo);

        public void AdicionarCep(Cep cep);
    }
}
