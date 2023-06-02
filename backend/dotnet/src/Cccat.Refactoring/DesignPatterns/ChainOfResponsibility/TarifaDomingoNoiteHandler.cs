using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.ChainOfResponsibility
{
    public class TarifaDomingoNoiteHandler : TarifaHandler
    {
        private const decimal TAXA_NOTURNA_DOMINGO = 5M;

        public TarifaDomingoNoiteHandler(TarifaHandler handler) : base(handler) { }

        public override decimal Calcular(Segmento segmento)
        {
            if (segmento.PeriodoNoturno() && segmento.EhDomingo())
                return segmento.Distancia * TAXA_NOTURNA_DOMINGO;

            return Next?.Calcular(segmento) ?? 0;
        }
    }
}
