namespace Cccat.Refactoring.ObjetoEstudoRefatorado
{
    public class Segmento
    {
        private const int PERIDO_NOTURNO_INICIO = 22;
        private const int PERIDO_NOTURNO_FIM = 6;

        public Segmento(int distancia, DateTime dataHora)
        {
            Distancia = distancia;
            DataHora = dataHora;

            Validar();
        }

        public int Distancia { get; private set; }

        public DateTime DataHora { get; private set; }

        public bool PeriodoNoturno()
            => DataHora.Hour >= PERIDO_NOTURNO_INICIO || DataHora.Hour <= PERIDO_NOTURNO_FIM;

        public bool EhDomingo()
            => DataHora.DayOfWeek == DayOfWeek.Sunday;

        public bool DistanciaValida()
            => Distancia > 0;

        public bool DataHoraValida()
            => DataHora != DateTime.MinValue;

        private void Validar()
        {
            if (!DistanciaValida())
                throw new ArgumentException("Distância inválida.");

            if (!DataHoraValida())
                throw new ArgumentException("Data hora inválida.");
        }
    }
}