using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Cccat.Infra.Repositories;
using Cccat.UseCases.Models;

namespace Cccat.UseCases
{
    public class Checkout
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICupomRepository _cupomRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public Checkout(IRepositoryFactory repositoryFactory)
        {
            _cupomRepository = repositoryFactory.CriarCupomRepository();
            _produtoRepository = repositoryFactory.CriarProdutoRepository();
            _pedidoRepository = repositoryFactory.CriarPedidoRepository();
        }

        public async Task<CheckoutOutputDto> Executar(CheckoutInputDto input)
        {
            var sequencia = await _pedidoRepository.ObterTotalPedidos() + 1;
            var pedido = new Pedido(input.IdPedido, input.Cpf, sequencia);

            input.Items.ForEach(item =>
            {
                var produto = _produtoRepository.Get(item.IdProduto);
                pedido.AdicionarItem(produto.Id, produto.Preco, item.Quantidade);

                if (!string.IsNullOrWhiteSpace(input.CepOrigem) && !string.IsNullOrWhiteSpace(input.CepDestino))
                {
                    var frete = item.Quantidade * CalculadoraFrete.Calcular(produto);
                    pedido.AdicionarFrete(frete);
                }

            });

            if (!string.IsNullOrWhiteSpace(input.Cupom))
            {
                var cupom = _cupomRepository.Get(input.Cupom);

                if (cupom is not null)
                    pedido.AdicionarCupom(cupom);
            }

            await _pedidoRepository.AdicionarPedido(pedido);

            return new CheckoutOutputDto
            {
                SubTotal = pedido.SubTotal,
                Frete = pedido.Frete,
                Total = pedido.Total
            };
        }
    }
}