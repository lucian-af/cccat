using Cccat.Application.Helpers;
using Cccat.Entities.Interfaces;

namespace Cccat.UseCases
{
    public class Checkout
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICupomRepository _cupomRepository;

        public Checkout(
            ICupomRepository cupomRepository,
            IProdutoRepository produtoRepository)
        {
            _cupomRepository = cupomRepository;
            _produtoRepository = produtoRepository;
        }

        public Output Execute(Input input)
        {
            if (ValidarCpf.Validar(input.Cpf))
            {
                var itensAgrupados = input.Items
                    .GroupBy(it => it.IdProduto)
                    .ToList();

                if (itensAgrupados.Any(grupoitem => grupoitem.Count() > 1))
                    throw new Exception("Só é permitido adicionar uma vez o mesmo item.");

                var subTotal = 0m;
                var frete = 0m;
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
                        var freteCalculado = produto.Volume() * 1000 * (produto.Densidade() / 100);
                        freteCalculado = Math.Max(10, freteCalculado);
                        frete += decimal.Truncate(freteCalculado * item.Quantidade);
                    }
                }

                decimal total = subTotal;
                if (!string.IsNullOrWhiteSpace(input.Cupom))
                {
                    var cupom = _cupomRepository.Get(input.Cupom);

                    if (cupom is not null && cupom.Validade >= DateTime.Now)
                        total -= total * cupom.Percentual / 100;
                }

                total += frete;

                return new Output
                {
                    SubTotal = subTotal,
                    Frete = frete,
                    Total = total,
                };
            }

            throw new Exception("CPF Inválido.");
        }
    }

    public class Output
    {
        public decimal SubTotal { get; set; }
        public decimal Frete { get; set; }
        public decimal Total { get; set; }
    }

    public class Input
    {
        public string Cpf { get; set; }
        public string Cupom { get; set; }
        public string CepOrigem { get; set; }
        public string CepDestino { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}