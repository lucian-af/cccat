using Cccat.Frete.Application.Models;
using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Domain.Interfaces;

namespace Cccat.Frete.Application.UseCase
{
    public class SimulaFrete
    {
        private const int DistanciaPadrao = 1000;

        public SimulaFrete(IRepositoryFactory repositoryFactory) { }

        public SimulaFreteOutputDto Simular(SimulaFreteInputDto input)
        {
            decimal frete = 0;
            input.Items.ForEach(item =>
            {
                if (!string.IsNullOrWhiteSpace(input.CepOrigem) && !string.IsNullOrWhiteSpace(input.CepDestino))
                {
                    var freteCalculado = CalculadoraFrete.Calcular(DistanciaPadrao, item.Volume, item.Densidade);
                    frete += freteCalculado * item.Quantidade;
                }
            });

            return new() { Frete = frete };
        }
    }
}
