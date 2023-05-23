namespace Cccat.Entities.Interfaces
{
    public interface ICepRepository
    {
        public Cep Get(string codigo);

        public void AdicionarCep(Cep cep);
    }
}
