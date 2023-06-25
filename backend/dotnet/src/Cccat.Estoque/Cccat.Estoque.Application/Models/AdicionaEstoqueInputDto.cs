namespace Cccat.Estoque.Application.Models
{
	public class AdicionaEstoqueInputDto
	{
		public List<AdicionaEstoqueItemInputDto> Itens { get; set; }
	}

	public class AdicionaEstoqueItemInputDto
	{
		public int IdProduto { get; set; }
		public int Quantidade { get; set; }
	}
}
