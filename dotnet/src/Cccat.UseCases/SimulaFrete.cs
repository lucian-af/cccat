using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Cccat.UseCases.Models;

namespace Cccat.UseCases
{
    public class SimulaFrete
    {
        private readonly IProdutoRepository _produtoRepository;

        public SimulaFrete(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
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
