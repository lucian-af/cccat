namespace Cccat.Entities
{
    public static class Coordenadas
    {
        public static decimal CalcularDistancia(Cep origem, Cep destino)
        {
            if (origem.Latitude == destino.Latitude && origem.Longitude == destino.Longitude)
                return 0;

            double raioLatitudeOrigem = ObterRaio(origem.Latitude);
            var raioLatitudeDestino = ObterRaio(destino.Latitude);

            var angulo = origem.Longitude - destino.Longitude;
            var raioAngulo = ObterRaio(angulo);

            var distancia = ObterDistanciaRadial(raioLatitudeOrigem, raioLatitudeDestino, raioAngulo);

            if (distancia > 1)
                distancia = 1;

            distancia = Math.Acos(distancia);
            distancia = distancia * 180 / Math.PI;
            distancia = distancia * 60 * 1.1515;
            distancia *= 1.609344;
            return (decimal)Math.Round(distancia, 2);
        }

        private static double ObterDistanciaRadial(double raioLatitudeOrigem, double raioLatitudeDestino, double raioTheta)
            => Math.Sin(raioLatitudeOrigem) * Math.Sin(raioLatitudeDestino) +
               Math.Cos(raioLatitudeOrigem) * Math.Cos(raioLatitudeDestino) *
               Math.Cos(raioTheta);

        private static double ObterRaio(double latitude)
            => Math.PI * latitude / 180;
    }
}
