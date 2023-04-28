using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.ChainOfResponsibility
{
    public abstract class TarifaHandler
    {
        public TarifaHandler Next;

        protected TarifaHandler(TarifaHandler? next)
            => Next = next;

        public abstract decimal Calcular(Segmento segmento);
    }
}
