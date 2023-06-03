namespace Cccat.Frete.Domain.Interfaces
{
    public interface IRepositoryFactory
    {
        public ICepRepository CriarCepRepository();
    }
}
