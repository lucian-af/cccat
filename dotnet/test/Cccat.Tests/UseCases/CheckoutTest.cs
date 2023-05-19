using Cccat.Tests.Fixtures;
using Cccat.UseCases;
using Cccat.UseCases.Models;

namespace Cccat.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class CheckoutTest
    {
        private readonly CheckoutFixture _checkoutFixture;
        private readonly Checkout _checkout;
        private readonly ConsultarPedido _consultarPedido;

        public CheckoutTest(DatabaseFixture dbFixture)
        {
            _checkoutFixture = new(dbFixture.DbContext);
            var produtoRepository = _checkoutFixture.CriarProdutoRepository(false);
            var cupomRepository = _checkoutFixture.CriarCupomRepository(false);
            var pedidoRepository = _checkoutFixture.CriarPedidoRepository(false);
            _checkout = new Checkout(cupomRepository, produtoRepository, pedidoRepository);
            _consultarPedido = new ConsultarPedido(pedidoRepository);
        }

        [Trait("Cccat", "UseCases.Checkout")]
        [Fact]
        public async Task NaoDeveCriarPedidoSeCpfInvalido()
        {
            var payload = new CheckoutInputDto { Cpf = "406.302.170-27" };

            var response = await Assert.ThrowsAsync<Exception>(async () => await _checkout.Executar(payload));

            Assert.NotNull(response);
            Assert.Equal("CPF Inválido.", response.Message);
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
            Assert.Equal("Quantidade do item inválida.", output.Message);
        }

        [Trait("Cccat", "UseCases.Checkout")]
        [Fact]
        public async Task NaoDevePermitirAdicionarItemDuplicado()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Items.Add(new CheckoutItemDto { IdProduto = 3, Quantidade = 1 });

            var output = await Assert.ThrowsAsync<Exception>(async () => await _checkout.Executar(payload));

            Assert.NotNull(output);
            Assert.Equal("Só é permitido adicionar uma vez o mesmo item.", output.Message);
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
        public async Task NaoDeveCriarPedidoSeDimensoesProdutoInvalidas()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Items.Clear();
            payload.Items.Add(new() { IdProduto = 4, Quantidade = 1 });

            var output = await Assert.ThrowsAsync<Exception>(async () => await _checkout.Executar(payload));

            Assert.NotNull(output);
            Assert.Equal("Produto inválido.", output.Message);
        }

        [Trait("Cccat", "UseCases.Checkout")]
        [Fact]
        public async Task NaoDeveCriarPedidoSePesoProdutoNegativo()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Items.Clear();
            payload.Items.Add(new() { IdProduto = 5, Quantidade = 1 });

            var output = await Assert.ThrowsAsync<Exception>(async () => await _checkout.Executar(payload));

            Assert.NotNull(output);
            Assert.Equal("Produto inválido.", output.Message);
        }

        [Trait("Cccat", "UseCases.Checkout")]
        [Fact]
        public async Task NaoDeveCriarPedidoSeProdutoInexistente()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Items.Clear();
            payload.Items.Add(new() { IdProduto = 999, Quantidade = 1 });

            var output = await Assert.ThrowsAsync<Exception>(async () => await _checkout.Executar(payload));

            Assert.NotNull(output);
            Assert.Equal("Produto não encontrado.", output.Message);
        }

        [Trait("Cccat", "UseCases.ConsultarPedido")]
        [Fact]
        public async Task DeveCriarPedidoCom3ItensEConsultarPedidoSalvo()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            var idPedido = Guid.NewGuid();

            payload.IdPedido = idPedido;

            await _checkout.Executar(payload);
            var pedido = _consultarPedido.Executar(payload.IdPedido);

            Assert.Equal(6090M, pedido.Total);
        }

        [Trait("Cccat", "UseCases.ConsultarPedido")]
        [Fact]
        public async Task DeveCriarPedidoCom3ItensEGerarCodigoPedido()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();

            _checkoutFixture.DeletarTodosPedidos();

            payload.IdPedido = Guid.NewGuid();
            await _checkout.Executar(payload);

            payload.IdPedido = Guid.NewGuid();
            await _checkout.Executar(payload);

            payload.IdPedido = Guid.NewGuid();
            await _checkout.Executar(payload);

            var pedido = _consultarPedido.Executar(payload.IdPedido);

            Assert.Equal("202300000003", pedido.Codigo);
        }
    }
}
