using Cccat.Application.Helpers;
using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Cccat.UseCases.Models;
using System.Text.RegularExpressions;

namespace Cccat.UseCases
{
    public class Checkout
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICupomRepository _cupomRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public Checkout(
            ICupomRepository cupomRepository,
            IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository)
        {
            _cupomRepository = cupomRepository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<CheckoutOutputDto> Executar(CheckoutInputDto input)
        {
            if (!ValidarCpf.Validar(input.Cpf))
                throw new Exception("CPF Inválido.");

            var itensAgrupados = input.Items
                .GroupBy(it => it.IdProduto)
                .ToList();

            if (itensAgrupados.Any(grupoitem => grupoitem.Count() > 1))
                throw new Exception("Só é permitido adicionar uma vez o mesmo item.");

            var subTotal = 0m;
            var frete = 0m;
            var pedidoItens = new List<PedidoItem>();
            foreach (var item in input.Items)
            {
                if (item.Quantidade <= 0)
                    throw new Exception("Quantidade do item inválida.");

                var produto = _produtoRepository.Get(item.IdProduto)
                    ?? throw new Exception("Produto não encontrado.");

                if (produto.Largura <= 0 || produto.Altura <= 0 || produto.Profundidade <= 0 || produto.Peso <= 0)
                    throw new Exception("Produto inválido.");

                subTotal += produto.Preco * item.Quantidade;

                if (!string.IsNullOrWhiteSpace(input.CepOrigem) && !string.IsNullOrWhiteSpace(input.CepDestino))
                {
                    var freteCalculado = CalculadoraFrete.Calcular(produto);
                    frete += decimal.Truncate(freteCalculado * item.Quantidade);
                }

                pedidoItens.Add(new()
                {
                    IdPedido = input.IdPedido,
                    IdProduto = produto.Id,
                    Quantidade = item.Quantidade,
                    Valor = produto.Preco * item.Quantidade
                });
            }

            decimal total = subTotal;
            if (!string.IsNullOrWhiteSpace(input.Cupom))
            {
                var cupom = _cupomRepository.Get(input.Cupom);

                if (cupom is not null && cupom.Validade >= DateTime.Now)
                    total -= total * cupom.Percentual / 100;
            }

            total += frete;

            var sequencia = await _pedidoRepository.ObterTotalPedidos() + 1;
            var codigo = $"{DateTime.Now.Year}{sequencia.ToString().PadLeft(8, '0')}";
            var pedido = new Pedido()
            {
                Id = input.IdPedido,
                Codigo = codigo,
                Cpf = new Regex("\\D").Replace(input.Cpf, ""),
                Frete = frete,
                Total = total,
                Itens = pedidoItens
            };

            await _pedidoRepository.AdicionarPedido(pedido);

            return new CheckoutOutputDto
            {
                SubTotal = subTotal,
                Frete = frete,
                Total = total,
            };
        }
    }
}