namespace Cccat.Domain.Entities
{
    public class Cupom
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public decimal Percentual { get; set; }
        public DateTime Validade { get; set; }
    }
}
