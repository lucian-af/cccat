namespace Cccat.Catalogo.Application.Models
{
    public class SimulaFreteInputDto
    {
        public string CepOrigem { get; set; }
        public string CepDestino { get; set; }
        public List<SimulaFreteItemDto> Items { get; set; }
    }

    public class SimulaFreteItemDto
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}