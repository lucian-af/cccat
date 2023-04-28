using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.StrategyFactory
{
    public class TarifaNormal : ITarifa
    {
        private const decimal TAXA_NORMAL = 2.1M;

        public decimal Calcular(Segmento segmento)
        {
            decimal tarifa = 0;

            if (!segmento.PeriodoNoturno() && !segmento.EhDomingo())
                tarifa = segmento.Distancia * TAXA_NORMAL;

            return tarifa;
        }
    }
}
