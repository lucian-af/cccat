using Cccat.Application.Models;
using Cccat.Domain.Entities;
using Cccat.Domain.Interfaces;

namespace Cccat.Application.UseCase
{
    public class SimulaFrete
    {
        private readonly IProdutoRepository _produtoRepository;

        public SimulaFrete(IRepositoryFactory repositoryFactory)
        {
            _produtoRepository = repositoryFactory.CriarProdutoRepository();
        }

        public SimulaFreteOutputDto Simular(SimulaFreteInputDto input)
        {
            decimal frete = 0;
            input.Items.ForEach(item =>
            {
                if (!string.IsNullOrWhiteSpace(input.CepOrigem) && !string.IsNullOrWhiteSpace(input.CepDestino))
                {
                    var produto = _produtoRepository.Get(item.IdProduto);
                    var freteCalculado = CalculadoraFrete.Calcular(produto);
                    frete += freteCalculado * item.Quantidade;
                }
            });

            return new() { Frete = frete };
        }
    }
}
