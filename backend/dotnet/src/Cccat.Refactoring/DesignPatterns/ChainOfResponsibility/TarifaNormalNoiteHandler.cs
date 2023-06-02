using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.ChainOfResponsibility
{
    public class TarifaNormalNoiteHandler : TarifaHandler
    {
        private const decimal TAXA_NOTURNA_NORMAL = 3.9M;

        public TarifaNormalNoiteHandler(TarifaHandler handler) : base(handler) { }

        public override decimal Calcular(Segmento segmento)
        {
            if (segmento.PeriodoNoturno() && !segmento.EhDomingo())
                return segmento.Distancia * TAXA_NOTURNA_NORMAL;

            return Next?.Calcular(segmento) ?? 0;
        }
    }
}
