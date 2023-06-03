namespace Cccat.Checkout.Domain.Entities
{
    public class Pedido
    {
        protected Pedido() { }

        public Guid Id { get; private set; }
        public string Codigo { get; private set; }
        public Cpf Cpf { get; private set; }
        public decimal Frete { get; private set; }
        public DateTime DataHora { get; private set; }
        public List<PedidoItem> Itens { get; private set; }
        public decimal SubTotal
        {
            get => Itens.Sum(item => item.Total());
            private set { }
        }
        public decimal Total
        {
            get => SubTotal + Frete - (Cupom?.CalcularDesconto(SubTotal) ?? 0);
            private set { }
        }
        public Cupom Cupom { get; private set; }

        public Pedido(Guid id, string cpf, long sequencia = 1)
        {
            Id = id;
            Cpf = new(cpf);
            DataHora = DateTime.Now;
            Codigo = $"{DataHora.Year}{sequencia.ToString().PadLeft(8, '0')}";
            Itens = new();
        }

        public void AdicionarItem(int idProduto, decimal preco, int quantidade)
        {
            if (Itens.Any(item => item.IdProduto == idProduto))
                throw new Exception("Não é permitido duplicar o mesmo item.");

            Itens.Add(new(Id, idProduto, preco, quantidade));
        }

        public void AdicionarCupom(Cupom cupom)
        {
            if (cupom.Valido(DataHora))
                Cupom = cupom;
        }

        public void AdicionarFrete(decimal valorFrete)
        {
            if (valorFrete <= 0)
                throw new Exception("Valor do frete deve ser maior que zero.");

            Frete += valorFrete;
        }
    }
}
