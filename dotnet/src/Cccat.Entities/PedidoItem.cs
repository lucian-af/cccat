namespace Cccat.Entities
{
    public class PedidoItem
    {
        public Guid Id { get; set; }
        public Guid IdPedido { get; set; }
        public int IdProduto { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public PedidoItem(Guid idPedido, int idProduto, decimal preco, int quantidade)
        {
            if (quantidade <= 0)
                throw new Exception("Quantidade inválida.");

            Id = Guid.NewGuid();
            IdPedido = idPedido;
            IdProduto = idProduto;
            Preco = preco;
            Quantidade = quantidade;
        }

        public decimal Total()
            => Preco * Quantidade;
    }
}
