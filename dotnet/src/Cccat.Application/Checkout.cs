using Cccat.Application.Helpers;
using Cccat.Infra;

namespace Cccat.Application
{
    public class Checkout
    {
        // TODO: separar nova camada
        private readonly DatabaseContext _context;

        public Checkout(DatabaseContext context)
        {
            _context = context;
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

                    var produto = _context.Produtos
                        .FirstOrDefault(produto => produto.Id == item.IdProduto);


                    if (produto != null)
                    {
                        if (produto.Largura <= 0 || produto.Altura <= 0 || produto.Profundidade <= 0 || produto.Peso <= 0)
                            throw new Exception("Produto inválido.");

                        subTotal += produto.Preco * item.Quantidade;

                        if (!string.IsNullOrWhiteSpace(input.CepOrigem) && !string.IsNullOrWhiteSpace(input.CepDestino))
                        {
                            var volume = produto.Largura / 100 * produto.Altura / 100 * produto.Profundidade / 100;
                            var densidade = produto.Peso / volume;
                            var freteCalculado = volume * 1000 * (densidade / 100);
                            freteCalculado = Math.Max(10, freteCalculado);
                            frete += decimal.Truncate(freteCalculado * item.Quantidade);
                        }
                    }
                }

                decimal total = subTotal;
                if (!string.IsNullOrWhiteSpace(input.Cupom))
                {
                    var cupom = _context.Cupons
                        .FirstOrDefault(cp => cp.Codigo.Equals(input.Cupom));

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