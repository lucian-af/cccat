﻿namespace Cccat.Checkout.Application.Models
{
	public class CheckoutOutputDto
	{
		public decimal SubTotal { get; set; }
		public decimal Frete { get; set; }
		public decimal Total { get; set; }
		public decimal Desconto { get; set; }
	}
}
