namespace Cccat.Entities
{
    public class PedidoItem
    {
        public Guid Id { get; set; }
        public Guid IdPedido { get; set; }
        public int IdProduto { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}
