namespace Cccat.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Cpf { get; set; }
        public decimal Total { get; set; }
        public decimal Frete { get; set; }
        public List<PedidoItem> Itens { get; set; } = new();
    }
}
