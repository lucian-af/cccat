namespace Cccat.Checkout.Application.Events;

public class PedidoRealizadoEvent
{
	public List<PedidoRealizadoItensEvent> Itens { get; set; } = new();
}

public class PedidoRealizadoItensEvent
{
	public int IdProduto { get; set; }
	public int Quantidade { get; set; }
}
