namespace Cccat.Frete.Domain.Services
{
	public static class CalculadoraFrete
	{
		private const decimal ValorFreteMinimo = 10m;

		public static decimal Calcular(decimal distancia, decimal volume, decimal densidade)
		{
			var freteCalculado = volume * distancia * (densidade / 100);
			return Math.Max(ValorFreteMinimo, Math.Round(freteCalculado, 2));
		}
	}
}
