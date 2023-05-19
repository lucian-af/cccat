namespace Cccat.UseCases.Models
{
    public class CheckoutOutputDto
    {
        public decimal SubTotal { get; set; }
        public decimal Frete { get; set; }
        public decimal Total { get; set; }
    }
}