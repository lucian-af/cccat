﻿namespace Cccat.Checkout.Application.Models
{
	public class ProdutoDto
	{
		public int Id { get; set; }
		public string Descricao { get; set; }
		public decimal Preco { get; set; }
		public decimal Largura { get; set; }
		public decimal Altura { get; set; }
		public decimal Profundidade { get; set; }
		public decimal Peso { get; set; }
		public decimal Densidade { get; set; }
		public decimal Volume { get; set; }
	}
}
