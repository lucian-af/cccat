namespace Cccat.Checkout.Domain.Entities
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Altura { get; private set; }
        public decimal Profundidade { get; private set; }
        public decimal Peso { get; private set; }

        public Produto(int id, string descricao, decimal preco, decimal largura, decimal altura, decimal profundidade, decimal peso)
        {
            if (largura <= 0 || altura <= 0 || profundidade <= 0)
                throw new Exception("Dimensões do produto inválidas.");

            if (peso <= 0)
                throw new Exception("Peso do produto inválido.");

            Id = id;
            Descricao = descricao;
            Preco = preco;
            Largura = largura;
            Altura = altura;
            Profundidade = profundidade;
            Peso = peso;
        }

        public decimal Volume()
            => Largura / 100 * (Altura / 100) * (Profundidade / 100);

        public decimal Densidade()
            => Peso / Volume();
    }
}
