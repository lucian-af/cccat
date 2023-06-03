using Cccat.Checkout.Domain.Interfaces;

namespace Cccat.Checkout.Application.UseCase
{
    public class ValidaCupom
    {
        private readonly ICupomRepository _cupomRepository;

        public ValidaCupom(IRepositoryFactory repositoryFactory)
            => _cupomRepository = repositoryFactory.CriarCupomRepository();

        public bool EhValido(string codigo)
        {
            var cupom = _cupomRepository.Get(codigo);
            return cupom.Valido(DateTime.Now);
        }
    }
}
