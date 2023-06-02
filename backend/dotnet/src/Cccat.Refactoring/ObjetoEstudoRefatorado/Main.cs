namespace Cccat.Refactoring.ObjetoEstudoRefatorado
{
    public static class Main
    {
        private const decimal TAXA_MINIMA = 10M;
        private const decimal TAXA_NORMAL = 2.1M;
        private const decimal TAXA_NORMAL_DOMINGO = 2.9M;
        private const decimal TAXA_NOTURNA_NORMAL = 3.9M;
        private const decimal TAXA_NOTURNA_DOMINGO = 5M;

        private static bool PeriodoNoturno(DateTime dataHora)
            => dataHora.Hour >= 22 || dataHora.Hour <= 6;

        private static bool EhDomingo(DateTime dataHora)
            => dataHora.DayOfWeek == DayOfWeek.Sunday;

        private static bool DistanciaValida(string distancia, out int distanciaValida)
        {
            if (int.TryParse(distancia, out distanciaValida))
                return distanciaValida > 0;

            return false;
        }

        private static bool DataHoraValida(string dataHora, out DateTime dataHoraValida)
        {
            if (DateTime.TryParse(dataHora, out dataHoraValida))
                return dataHoraValida != DateTime.MinValue;

            return false;
        }

        public static decimal CalcularCorrida(List<Parametros> parametros)
        {
            var valorTotalCorridas = 0M;
            foreach (var parametro in parametros)
            {
                if (!DistanciaValida(parametro.Distancia, out var distancia))
                    throw new ArgumentException("Distância inválida.");

                if (!DataHoraValida(parametro.DataHora, out var dataHora))
                    throw new ArgumentException("DataHora inválida.");

                if (PeriodoNoturno(dataHora) && !EhDomingo(dataHora))
                    valorTotalCorridas += distancia * TAXA_NOTURNA_NORMAL;

                if (PeriodoNoturno(dataHora) && EhDomingo(dataHora))
                    valorTotalCorridas += distancia * TAXA_NOTURNA_DOMINGO;

                if (!PeriodoNoturno(dataHora) && EhDomingo(dataHora))
                    valorTotalCorridas += distancia * TAXA_NORMAL_DOMINGO;

                if (!PeriodoNoturno(dataHora) && !EhDomingo(dataHora))
                    valorTotalCorridas += distancia * TAXA_NORMAL;
            }

            return valorTotalCorridas < TAXA_MINIMA ? TAXA_MINIMA : valorTotalCorridas;
        }
    }

    public class Parametros
    {
        public string Distancia { get; set; }

        public string DataHora { get; set; }
    }
}