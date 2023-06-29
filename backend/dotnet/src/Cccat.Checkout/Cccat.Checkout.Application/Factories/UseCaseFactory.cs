using Cccat.Checkout.Application.Queue;
using Cccat.Checkout.Domain.Interfaces;
using US = Cccat.Checkout.Application.UseCase;

namespace Cccat.Checkout.Application.Factories
{
	public class UseCaseFactory
	{
		private readonly IRepositoryFactory _repositoryFactory;
		private readonly IGatewayFactory _gatewayFactory;
		private readonly IQueue _queue;

		public UseCaseFactory(IRepositoryFactory repositoryFactory, IGatewayFactory gatewayFactory, IQueue queue)
		{
			_repositoryFactory = repositoryFactory;
			_gatewayFactory = gatewayFactory;
			_queue = queue;
		}

		public US.Checkout CriarCheckout() => new(_repositoryFactory, _gatewayFactory, _queue);
	}
}
