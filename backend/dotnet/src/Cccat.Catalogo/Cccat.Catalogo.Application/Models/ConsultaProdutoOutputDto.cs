namespace Cccat.Catalogo.Application.Models
{
    public class ConsultaProdutoOutputDto
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public decimal Profundidade { get; set; }
        public decimal Peso { get; set; }
        public decimal Volume { get; set; }
        public decimal Densidade { get; set; }
    }
}