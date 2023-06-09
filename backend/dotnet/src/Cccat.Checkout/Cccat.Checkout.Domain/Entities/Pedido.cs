﻿namespace Cccat.Checkout.Domain.Entities
{
	public class Pedido
	{
		private Pedido() { }

		public Guid Id { get; private set; }
		public string Codigo { get; private set; }
		public Cpf Cpf { get; private set; }
		public decimal Frete { get; private set; }
		public DateTime DataHora { get; private set; }
		public List<PedidoItem> Itens { get; private set; }
		public decimal Desconto { get; private set; }
		public decimal SubTotal
		{
			get => Itens.Sum(item => item.Total());
			private set { }
		}
		public decimal Total
		{
			get => SubTotal + Frete - Desconto;
			private set { }
		}
		public int? IdCupom { get; private set; }

		public Pedido(Guid id, string cpf, long sequencia = 1)
		{
			Id = id;
			Cpf = new(cpf);
			DataHora = DateTime.Now;
			Codigo = new PedidoCodigo(DataHora, sequencia).Codigo;
			Itens = new();
		}

		public void AdicionarItem(int idProduto, decimal preco, int quantidade)
		{
			if (Itens.Any(item => item.IdProduto == idProduto))
				throw new Exception("Não é permitido duplicar o mesmo item.");

			Itens.Add(new(Id, idProduto, preco, quantidade));
		}

		public void AdicionarCupom(Cupom cupom)
		{
			if (cupom.Valido(DataHora))
			{
				IdCupom = cupom.Id;
				Desconto = cupom.CalcularDesconto(SubTotal);
			}
		}

		public void AdicionarFrete(decimal valorFrete)
		{
			if (valorFrete <= 0)
				throw new Exception("Valor do frete deve ser maior que zero.");

			Frete += valorFrete;
		}
	}
}
