namespace Cccat.Checkout.Domain.Interfaces
{
	public interface IRepositoryFactory
	{
		public IPedidoRepository CriarPedidoRepository();
		public ICupomRepository CriarCupomRepository();
	}
}
