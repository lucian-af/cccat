namespace Cccat.Refactoring.ObjetoEstudoRefatorado
{
    public class Corrida
    {
        private const decimal TAXA_MINIMA = 10M;
        private const decimal TAXA_NORMAL = 2.1M;
        private const decimal TAXA_NORMAL_DOMINGO = 2.9M;
        private const decimal TAXA_NOTURNA_NORMAL = 3.9M;
        private const decimal TAXA_NOTURNA_DOMINGO = 5M;

        public List<Segmento> Segmentos { get; private set; } = new();

        public void AdicionarSegmento(int distancia, DateTime dataHora)
            => Segmentos.Add(new(distancia, dataHora));

        public decimal CalcularTarifa()
        {
            var valorTotalCorridas = 0M;
            foreach (var segmento in Segmentos)
            {
                if (segmento.PeriodoNoturno() && !segmento.EhDomingo())
                    valorTotalCorridas += segmento.Distancia * TAXA_NOTURNA_NORMAL;

                if (segmento.PeriodoNoturno() && segmento.EhDomingo())
                    valorTotalCorridas += segmento.Distancia * TAXA_NOTURNA_DOMINGO;

                if (!segmento.PeriodoNoturno() && segmento.EhDomingo())
                    valorTotalCorridas += segmento.Distancia * TAXA_NORMAL_DOMINGO;

                if (!segmento.PeriodoNoturno() && !segmento.EhDomingo())
                    valorTotalCorridas += segmento.Distancia * TAXA_NORMAL;
            }

            return valorTotalCorridas < TAXA_MINIMA ? TAXA_MINIMA : valorTotalCorridas;
        }
    }
}