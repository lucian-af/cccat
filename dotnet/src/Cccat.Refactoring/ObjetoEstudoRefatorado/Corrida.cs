using Cccat.Refactoring.DesignPatterns.StrategyFactory;

namespace Cccat.Refactoring.ObjetoEstudoRefatorado
{
    public class Corrida
    {
        private const decimal TAXA_MINIMA = 10M;

        public List<Segmento> Segmentos { get; private set; } = new();

        public void AdicionarSegmento(int distancia, DateTime dataHora)
            => Segmentos.Add(new(distancia, dataHora));

        public decimal CalcularTarifa()
        {
            var valorTotalCorridas = 0M;
            foreach (var segmento in Segmentos)
            {
                var tarifa = TarifaFactory.Criar(segmento);
                valorTotalCorridas += tarifa.Calcular(segmento);
            }

            return valorTotalCorridas < TAXA_MINIMA ? TAXA_MINIMA : valorTotalCorridas;
        }
    }
}