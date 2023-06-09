namespace Cccat.Catalogo.Domain.Entities
{
	public class Produto
	{
		public int Id { get; private set; }
		public string Descricao { get; private set; }
		public decimal Preco { get; private set; }
		public ProdutoDimensao Dimensao { get; set; }
		public decimal Peso { get; private set; }

		private Produto() { }

		public Produto(int id, string descricao, decimal preco, decimal largura, decimal altura, decimal profundidade, decimal peso)
		{
			if (peso <= 0)
				throw new Exception("Peso do produto inválido.");

			Id = id;
			Descricao = descricao;
			Preco = preco;
			Dimensao = new(largura, altura, profundidade);
			Peso = peso;
		}

		public decimal Volume()
			=> Dimensao.Largura / 100 * (Dimensao.Altura / 100) * (Dimensao.Profundidade / 100);

		public decimal Densidade()
			=> Peso / Volume();
	}
}
