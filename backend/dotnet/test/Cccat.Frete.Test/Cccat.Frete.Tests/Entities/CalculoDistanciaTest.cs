using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Domain.Services;

namespace Cccat.Frete.Tests.Entities
{
	public class CalculoDistanciaTest
	{
		[Trait("Cccat", "Entities.Frete.CalculoDistancia")]
		[Fact]
		public void DeveCalcularDistanciaEntreCoordenadas()
		{
			var origem = new Coordenada(-23.9900, -55.7762);
			var destino = new Coordenada(-13.2342, -29.3811);

			var distancia = CalculoDistancia.Calcular(origem, destino);

			Assert.Equal(3020.17M, distancia);
		}

		[Trait("Cccat", "Entities.Frete.CalculoDistancia")]
		[Fact]
		public void NaoDeveCalcularDistanciaEntreCoordenadasSeCoordenadasIguais()
		{
			var origem = new Coordenada(-23.9900, -21.7762);
			var destino = origem;

			var distancia = CalculoDistancia.Calcular(origem, destino);

			Assert.Equal(0, distancia);
		}
	}
}
