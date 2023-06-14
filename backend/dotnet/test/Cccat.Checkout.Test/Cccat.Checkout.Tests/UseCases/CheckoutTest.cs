using Cccat.Checkout.Application.Models;
using Cccat.Checkout.Infra.Factories;
using Cccat.Checkout.Tests.Fixtures;
using US = Cccat.Checkout.Application.UseCase;

namespace Cccat.Checkout.Tests.UseCases
{
	[Collection(nameof(DatabaseFixtureCollection))]
	public class CheckoutTest
	{
		private readonly DatabaseRepositoryFactoryFixture _databaseRepositoryFactory;
		private readonly CheckoutFixture _checkoutFixture;
		private readonly US.Checkout _checkout;
		private readonly US.ConsultaPedido _consultarPedido;

		public CheckoutTest(DatabaseFixture dbFixture)
		{
			_databaseRepositoryFactory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext);
			var factory = _databaseRepositoryFactory.CriarRepositoryFactory();
			_checkoutFixture = new();
			var gatewayFactory = new GatewayHttpFactory(_checkoutFixture.ServiceProvider);
			_checkout = new US.Checkout(factory, gatewayFactory);
			_consultarPedido = new US.ConsultaPedido(factory);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task NaoDeveCriarPedidoSeCpfInvalido()
		{
			var payload = new CheckoutInputDto { Cpf = "406.302.170-27" };

			var response = await Assert.ThrowsAsync<Exception>(async () => await _checkout.Executar(payload));

			Assert.NotNull(response);
			Assert.Equal("Cpf inválido.", response.Message);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task DeveCriarPedidoCom3Itens()
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();

			var output = await _checkout.Executar(payload);

			Assert.Equal(6090M, output.Total);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task DeveCriarPedidoCom3ItensComCupomDesconto()
		{
			var payload = _checkoutFixture.CriarInputValidoComCupom();

			var output = await _checkout.Executar(payload);

			Assert.Equal(4872M, output.Total);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task NaoDeveAplicarDescontoSeCupomExpirado()
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();
			payload.Cupom = "VALE50";

			var output = await _checkout.Executar(payload);

			Assert.Equal(6090M, output.Total);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task NaoDeveAplicarDescontoSeCupomInexistente()
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();
			payload.Cupom = "VALE100";

			var output = await _checkout.Executar(payload);

			Assert.Equal(6090M, output.Total);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task NaoDevePermitirItemComQuantidadeNegativa()
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();
			payload.Items.Clear();
			payload.Items.Add(new CheckoutItemDto { IdProduto = 3, Quantidade = -3 });

			var output = await Assert.ThrowsAsync<Exception>(async () => await _checkout.Executar(payload));

			Assert.NotNull(output);
			Assert.Equal("Quantidade inválida.", output.Message);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task NaoDevePermitirAdicionarItemDuplicado()
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();
			payload.Items.Add(new CheckoutItemDto { IdProduto = 3, Quantidade = 1 });

			var output = await Assert.ThrowsAsync<Exception>(async () => await _checkout.Executar(payload));

			Assert.NotNull(output);
			Assert.Equal("Não é permitido duplicar o mesmo item.", output.Message);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task DeveAcrescerValorFreteNoPedido()
		{
			var payload = _checkoutFixture.CriarInputValidoComFrete();
			payload.Items.RemoveAll(item => item.IdProduto == 3);

			var output = await _checkout.Executar(payload);

			Assert.Equal(6000M, output.SubTotal);
			Assert.Equal(250M, output.Frete);
			Assert.Equal(6250M, output.Total);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task DeveAcrescerValorFreteMinimoNoPedido()
		{
			var payload = _checkoutFixture.CriarInputValidoComFrete();

			var output = await _checkout.Executar(payload);

			Assert.Equal(6090M, output.SubTotal);
			Assert.Equal(280M, output.Frete);
			Assert.Equal(6370M, output.Total);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task DeveCriarPedidoCom3ItensEConsultarPedidoSalvo()
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();
			var idPedido = Guid.NewGuid();

			payload.IdPedido = idPedido;

			await _checkout.Executar(payload);
			var pedido = _consultarPedido.ConsultaPorId(payload.IdPedido);

			Assert.Equal(6090M, pedido.SubTotal);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task DeveCriarPedidoCom3ItensEGerarCodigoPedido()
		{
			var payload = _checkoutFixture.CriarInputValidoSomenteItens();

			_databaseRepositoryFactory.DeletarTodosPedidos();

			payload.IdPedido = Guid.NewGuid();
			await _checkout.Executar(payload);

			payload.IdPedido = Guid.NewGuid();
			await _checkout.Executar(payload);

			payload.IdPedido = Guid.NewGuid();
			await _checkout.Executar(payload);

			var pedido = _consultarPedido.ConsultaPorId(payload.IdPedido);

			Assert.Equal("202300000003", pedido.Codigo);
		}

		[Trait("Cccat", "UseCases.Checkout")]
		[Fact]
		public async Task DeveCriarPedidoCom3ItensComCupomEFrete()
		{
			var payload = _checkoutFixture.CriarInputValidoComCupomEFrete();

			var output = await _checkout.Executar(payload);

			Assert.Equal(6090M, output.SubTotal);
			Assert.Equal(203.99M, output.Frete);
			Assert.Equal(5075.99M, output.Total);
			Assert.Equal(1218M, output.Desconto);
		}
	}
}
