using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.StrategyFactory
{
    public class TarifaDomingoNoite : ITarifa
    {
        private const decimal TAXA_NOTURNA_DOMINGO = 5M;

        public decimal Calcular(Segmento segmento)
        {
            decimal tarifa = 0;

            if (segmento.PeriodoNoturno() && segmento.EhDomingo())
                tarifa = segmento.Distancia * TAXA_NOTURNA_DOMINGO;

            return tarifa;
        }
    }
}
