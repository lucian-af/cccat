using Cccat.Application.Models;
using Cccat.Domain.Entities;
using Cccat.Domain.Interfaces;

namespace Cccat.Application.UseCase
{
    public class CalculaDistancia
    {
        private readonly ICepRepository _cepRepository;

        public CalculaDistancia(IRepositoryFactory repositoryFactory)
            => _cepRepository = repositoryFactory.CriarCepRepository();

        public CalculaDistanciaOutputDto Calcular(CalculaDistanciaInputDto input)
        {
            var cepOrigem = _cepRepository.Get(input.CepOrigem);
            var cepDestino = _cepRepository.Get(input.CepDestino);

            var resultado = Coordenadas.CalcularDistancia(cepOrigem, cepDestino);

            return new() { Valor = resultado };
        }
    }
}
