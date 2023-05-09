namespace Cccat.Entities.Negocio
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public decimal Profundidade { get; set; }
        public decimal Peso { get; set; }

        public decimal Volume()
            => Largura / 100 * (Altura / 100) * (Profundidade / 100);

        public decimal Densidade()
            => Peso / Volume();
    }
}
