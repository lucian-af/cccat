namespace Cccat.Checkout.Domain.Entities
{
	public class PedidoCodigo
	{
		public string Codigo { get; private set; }

		public PedidoCodigo(DateTime dataHora, long sequencia = 1)
			=> Codigo = $"{dataHora.Year}{sequencia.ToString().PadLeft(8, '0')}";
	}
}
