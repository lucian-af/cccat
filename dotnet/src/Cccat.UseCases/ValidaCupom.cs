using Cccat.Entities.Interfaces;

namespace Cccat.UseCases
{
    public class ValidaCupom
    {
        private readonly ICupomRepository _cupomRepository;

        public ValidaCupom(ICupomRepository cupomRepository)
            => _cupomRepository = cupomRepository;

        public bool EhValido(string codigo)
        {
            var cupom = _cupomRepository.Get(codigo);
            return cupom.Valido(DateTime.Now);
        }
    }
}
