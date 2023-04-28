using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.StrategyFactory
{
    public class TarifaNormalDomingo : ITarifa
    {
        private const decimal TAXA_NORMAL_DOMINGO = 2.9M;

        public decimal Calcular(Segmento segmento)
        {
            decimal tarifa = 0;

            if (!segmento.PeriodoNoturno() && segmento.EhDomingo())
                tarifa = segmento.Distancia * TAXA_NORMAL_DOMINGO;

            return tarifa;
        }
    }
}
