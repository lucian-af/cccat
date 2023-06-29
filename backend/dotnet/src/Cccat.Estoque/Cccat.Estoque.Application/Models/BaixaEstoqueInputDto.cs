namespace Cccat.Estoque.Application.Models
{
	public class BaixaEstoqueInputDto
	{
		public List<BaixaEstoqueItemInputDto> Itens { get; set; } = new();
	}

	public class BaixaEstoqueItemInputDto
	{
		public int IdProduto { get; set; }
		public int Quantidade { get; set; }
	}
}
