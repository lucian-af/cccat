using Cccat.Catalogo.Application.UseCase;
using Cccat.Catalogo.Domain.Interfaces;

namespace Cccat.Catalogo.Application.Factories
{
    public class UseCaseFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UseCaseFactory(IRepositoryFactory repositoryFactory)
            => _repositoryFactory = repositoryFactory;

        public Checkout CriarCheckout() => new(_repositoryFactory);

        public ConsultaProduto CriarConsultaProduto() => new(_repositoryFactory);
    }
}
