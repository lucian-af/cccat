using Cccat.Frete.Application.Models;
using Cccat.Frete.Domain.Interfaces;
using Cccat.Frete.Domain.Services;

namespace Cccat.Frete.Application.UseCase
{
	public class SimulaFrete
	{
		private const decimal DistanciaPadrao = 1000m;
		private readonly ICepRepository _cepRepository;

		public SimulaFrete(IRepositoryFactory repositoryFactory)
			=> _cepRepository = repositoryFactory.CriarCepRepository();

		public SimulaFreteOutputDto Simular(SimulaFreteInputDto input)
		{
			if (string.IsNullOrWhiteSpace(input.CepOrigem) || string.IsNullOrWhiteSpace(input.CepDestino))
				return new() { Frete = 0 };

			var distancia = DistanciaPadrao;
			var cepOrigem = _cepRepository.ObterPorCodigo(input.CepOrigem);
			var cepDestino = _cepRepository.ObterPorCodigo(input.CepDestino);

			if (cepOrigem is not null && cepDestino is not null)
				distancia = CalculoDistancia.Calcular(cepOrigem.Coordenadas, cepDestino.Coordenadas);

			decimal frete = 0;
			input.Items.ForEach(item =>
			{
				var freteCalculado = CalculadoraFrete.Calcular(distancia, item.Volume, item.Densidade);
				frete += freteCalculado * item.Quantidade;
			});

			return new() { Frete = frete };
		}
	}
}
