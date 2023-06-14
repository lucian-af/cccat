namespace Cccat.Frete.Domain.Entities
{
	public class Coordenada
	{
		public double Latitude { get; private set; }
		public double Longitude { get; private set; }

		public Coordenada(double latitude, double longitude)
		{
			if (latitude == longitude)
				throw new Exception("Coordenada inválida.");

			Latitude = latitude;
			Longitude = longitude;
		}
	}
}
