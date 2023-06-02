using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.ChainOfResponsibility
{
    public class TarifaNormalHandler : TarifaHandler
    {
        private const decimal TAXA_NORMAL = 2.1M;

        public TarifaNormalHandler(TarifaHandler handler) : base(handler) { }

        public override decimal Calcular(Segmento segmento)
        {
            if (!segmento.PeriodoNoturno() && !segmento.EhDomingo())
                return segmento.Distancia * TAXA_NORMAL;

            return Next?.Calcular(segmento) ?? 0;
        }
    }
}
