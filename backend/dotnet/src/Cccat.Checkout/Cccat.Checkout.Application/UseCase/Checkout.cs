using Cccat.Checkout.Application.Factories;
using Cccat.Checkout.Application.Gateways;
using Cccat.Checkout.Application.Models;
using Cccat.Checkout.Domain.Entities;
using Cccat.Checkout.Domain.Interfaces;

namespace Cccat.Checkout.Application.UseCase
{
    public class Checkout
    {
        private readonly ICupomRepository _cupomRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICatalogoGateway _catalogoGateway;
        private readonly IFreteGateway _freteGateway;

        public Checkout(IRepositoryFactory repositoryFactory, IGatewayFactory gatewayFactory)
        {
            _cupomRepository = repositoryFactory.CriarCupomRepository();
            _pedidoRepository = repositoryFactory.CriarPedidoRepository();
            _catalogoGateway = gatewayFactory.CriarCatalogoGateway();
            _freteGateway = gatewayFactory.CriarFreteGateway();
        }

        public async Task<CheckoutOutputDto> Executar(CheckoutInputDto input)
        {
            var sequencia = await _pedidoRepository.ObterTotalPedidos() + 1;
            var pedido = new Pedido(input.IdPedido, input.Cpf, sequencia);

            var simulaFrete = new SimulaFreteDto
            {
                Items = new(),
                CepOrigem = input.CepOrigem,
                CepDestino = input.CepDestino,
            };

            foreach (var item in input.Items)
            {
                var produto = await _catalogoGateway.ConsultarProduto(item.IdProduto);
                pedido.AdicionarItem(produto.Id, produto.Preco, item.Quantidade);
                simulaFrete.Items.Add(new()
                {
                    Densidade = produto.Densidade,
                    Volume = produto.Volume,
                    Quantidade = item.Quantidade
                });
            }

            if (!string.IsNullOrWhiteSpace(input.CepOrigem) && !string.IsNullOrWhiteSpace(input.CepDestino))
            {
                var freteSimulado = await _freteGateway.Simularfrete(simulaFrete);
                pedido.AdicionarFrete(freteSimulado.Frete);
            }

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