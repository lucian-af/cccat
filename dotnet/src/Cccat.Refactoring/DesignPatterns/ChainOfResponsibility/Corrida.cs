using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.DesignPatterns.ChainOfResponsibility
{
    public class Corrida
    {
        private const decimal TAXA_MINIMA = 10M;
        private readonly TarifaHandler _tarifaHandler;

        public List<Segmento> Segmentos { get; private set; } = new();

        public Corrida(TarifaHandler tarifaHandler)
            => _tarifaHandler = tarifaHandler;

        public void AdicionarSegmento(int distancia, DateTime dataHora)
            => Segmentos.Add(new(distancia, dataHora));

        public decimal CalcularTarifa()
        {
            var valorTotalCorridas = 0M;

            Segmentos.ForEach(segmento => valorTotalCorridas += _tarifaHandler.Calcular(segmento));

            return valorTotalCorridas < TAXA_MINIMA ? TAXA_MINIMA : valorTotalCorridas;
        }
    }
}