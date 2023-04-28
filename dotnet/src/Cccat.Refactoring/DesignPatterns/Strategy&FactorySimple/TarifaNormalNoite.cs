using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.StrategyFactory
{
    public class TarifaNormalNoite : ITarifa
    {
        private const decimal TAXA_NOTURNA_NORMAL = 3.9M;

        public decimal Calcular(Segmento segmento)
        {
            decimal tarifa = 0;

            if (segmento.PeriodoNoturno() && !segmento.EhDomingo())
                tarifa = segmento.Distancia * TAXA_NOTURNA_NORMAL;

            return tarifa;
        }
    }
}
