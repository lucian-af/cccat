namespace Cccat.Catalogo.Domain.Entities
{
	public class ProdutoDimensao
	{
		public decimal Largura { get; private set; }
		public decimal Altura { get; private set; }
		public decimal Profundidade { get; private set; }

		public ProdutoDimensao(decimal largura, decimal altura, decimal profundidade)
		{
			if (largura <= 0 || altura <= 0 || profundidade <= 0)
				throw new Exception("Dimensões do produto inválidas.");

			Largura = largura;
			Altura = altura;
			Profundidade = profundidade;
		}

		public decimal Volume()
			=> Largura / 100 * (Altura / 100) * (Profundidade / 100);
	}
}
