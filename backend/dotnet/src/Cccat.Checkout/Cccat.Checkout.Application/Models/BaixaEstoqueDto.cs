namespace Cccat.Checkout.Application.Models
{
	public class BaixaEstoqueDto
	{
		public List<BaixaEstoqueItemDto> Itens { get; set; } = new();
	}

	public class BaixaEstoqueItemDto
	{
		public int IdProduto { get; set; }
		public int Quantidade { get; set; }
	}
}
