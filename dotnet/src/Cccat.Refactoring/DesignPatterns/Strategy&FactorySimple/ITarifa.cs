using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.StrategyFactory
{
    public interface ITarifa
    {
        public decimal Calcular(Segmento segmento);
    }
}
