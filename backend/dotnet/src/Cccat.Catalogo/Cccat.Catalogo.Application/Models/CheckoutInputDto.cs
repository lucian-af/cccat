namespace Cccat.Catalogo.Application.Models
{
    public class CheckoutInputDto
    {
        public Guid IdPedido { get; set; }
        public string Cpf { get; set; }
        public string Cupom { get; set; }
        public string CepOrigem { get; set; }
        public string CepDestino { get; set; }
        public List<CheckoutItemDto> Items { get; set; }
    }

    public class CheckoutItemDto
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}