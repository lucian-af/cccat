using Cccat.Domain.Entities;

namespace Cccat.Domain.Interfaces
{
    public interface ICepRepository
    {
        public Cep Get(string codigo);

        public void AdicionarCep(Cep cep);
    }
}
