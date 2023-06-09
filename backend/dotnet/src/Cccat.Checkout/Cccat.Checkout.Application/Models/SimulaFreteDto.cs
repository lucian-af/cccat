namespace Cccat.Checkout.Application.Models
{
    public class SimulaFreteDto
    {
        public string CepOrigem { get; set; }
        public string CepDestino { get; set; }
        public List<SimulaFreteItemDto> Items { get; set; }
    }

    public class SimulaFreteItemDto
    {
        public decimal Volume { get; set; }
        public decimal Densidade { get; set; }
        public int Quantidade { get; set; }
    }
}