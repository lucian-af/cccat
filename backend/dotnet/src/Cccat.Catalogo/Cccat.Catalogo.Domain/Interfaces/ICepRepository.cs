using Cccat.Catalogo.Domain.Entities;

namespace Cccat.Catalogo.Domain.Interfaces
{
    public interface ICepRepository
    {
        public Cep Get(string codigo);

        public void AdicionarCep(Cep cep);
    }
}
