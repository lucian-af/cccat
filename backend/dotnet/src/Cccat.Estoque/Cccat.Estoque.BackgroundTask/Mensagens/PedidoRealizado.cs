namespace Cccat.Estoque.BackgroundTask.Mensagens;
public class PedidoRealizado
{
	public List<PedidoRealizadoItens> Itens { get; set; }
}

public class PedidoRealizadoItens
{
	public int IdProduto { get; set; }
	public int Quantidade { get; set; }
}
