using Cccat.Frete.Domain.Entities;

namespace Cccat.Frete.Domain.Services
{
	public static class CalculoDistancia
	{
		public static decimal Calcular(Coordenada origem, Coordenada destino)
		{
			if (origem.Latitude == destino.Latitude && origem.Longitude == destino.Longitude)
				return 0;

			var raioLatitudeOrigem = origem.Latitude.ToRadius();
			var raioLatitudeDestino = destino.Latitude.ToRadius();

			var angulo = origem.Longitude - destino.Longitude;
			var raioAngulo = angulo.ToRadius();

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

		private static double ToRadius(this double latitude)
			=> Math.PI * latitude / 180;
	}
}
