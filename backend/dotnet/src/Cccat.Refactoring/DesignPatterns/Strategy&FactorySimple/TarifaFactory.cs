using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.StrategyFactory
{
    public static class TarifaFactory
    {
        public static ITarifa Criar(Segmento segmento)
        {
            if (segmento.PeriodoNoturno() && !segmento.EhDomingo())
                return new TarifaNormalNoite();

            if (segmento.PeriodoNoturno() && segmento.EhDomingo())
                return new TarifaDomingoNoite();

            if (!segmento.PeriodoNoturno() && segmento.EhDomingo())
                return new TarifaNormalDomingo();

            if (!segmento.PeriodoNoturno() && !segmento.EhDomingo())
                return new TarifaNormal();

            throw new NotImplementedException("Tarifa não criada.");
        }
    }
}
