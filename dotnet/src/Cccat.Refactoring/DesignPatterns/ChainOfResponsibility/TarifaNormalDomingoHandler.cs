using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.ChainOfResponsibility
{
    public class TarifaNormalDomingoHandler : TarifaHandler
    {
        private const decimal TAXA_NORMAL_DOMINGO = 2.9M;

        public TarifaNormalDomingoHandler(TarifaHandler handler) : base(handler) { }

        public override decimal Calcular(Segmento segmento)
        {
            if (!segmento.PeriodoNoturno() && segmento.EhDomingo())
                return segmento.Distancia * TAXA_NORMAL_DOMINGO;

            return Next?.Calcular(segmento) ?? 0;
        }
    }
}
