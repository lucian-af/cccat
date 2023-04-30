using Cccat.Application;
using Cccat.Infra;
using Cccat.Infra.Seed;
using Cccat.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Tests.Application
{
    [Collection(nameof(CheckoutFixtureCollection))]
    public class CheckoutTest
    {
        private readonly CheckoutFixture _checkoutFixture;
        private readonly Checkout _checkout;

        public CheckoutTest(CheckoutFixture checkoutFixture)
        {
            _checkoutFixture = checkoutFixture;
            var dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseInMemoryDatabase("CCCAT");
            var dbContext = new DatabaseContext(dbOptions.Options);
            SeedData.CriarDados(dbContext).Wait();
            _checkout = new Checkout(dbContext);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void NaoDeveCriarPedidoSeCpfInvalido()
        {
            var payload = new Input { Cpf = "406.302.170-27" };

            var response = Assert.Throws<Exception>(() => _checkout.Execute(payload));

            Assert.NotNull(response);
            Assert.Equal("CPF Inválido.", response.Message);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void DeveCriarPedidoCom3Itens()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();

            var output = _checkout.Execute(payload);

            Assert.Equal(6090M, output.Total);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void DeveCriarPedidoCom3ItensComCupomDesconto()
        {
            var payload = _checkoutFixture.CriarInputValidoComCupom();

            var output = _checkout.Execute(payload);

            Assert.Equal(4872M, output.Total);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void NaoDeveAplicarDescontoSeCupomExpirado()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Cupom = "VALE50";

            var output = _checkout.Execute(payload);

            Assert.Equal(6090M, output.Total);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void NaoDeveAplicarDescontoSeCupomInexistente()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Cupom = "VALE100";

            var output = _checkout.Execute(payload);

            Assert.Equal(6090M, output.Total);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void NaoDevePermitirItemComQuantidadeNegativa()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Items.Clear();
            payload.Items.Add(new Item { IdProduto = 3, Quantidade = -3 });

            var output = Assert.Throws<Exception>(() => _checkout.Execute(payload));

            Assert.NotNull(output);
            Assert.Equal("Quantidade do item inválida.", output.Message);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void NaoDevePermitirAdicionarItemDuplicado()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Items.Add(new Item { IdProduto = 3, Quantidade = 1 });

            var output = Assert.Throws<Exception>(() => _checkout.Execute(payload));

            Assert.NotNull(output);
            Assert.Equal("Só é permitido adicionar uma vez o mesmo item.", output.Message);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void DeveAcrescerValorFreteNoPedido()
        {
            var payload = _checkoutFixture.CriarInputValidoComFrete();
            payload.Items.RemoveAll(item => item.IdProduto == 3);

            var output = _checkout.Execute(payload);

            Assert.Equal(6000M, output.SubTotal);
            Assert.Equal(250M, output.Frete);
            Assert.Equal(6250M, output.Total);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void DeveAcrescerValorFreteMinimoNoPedido()
        {
            var payload = _checkoutFixture.CriarInputValidoComFrete();

            var output = _checkout.Execute(payload);

            Assert.Equal(6090M, output.SubTotal);
            Assert.Equal(280M, output.Frete);
            Assert.Equal(6370M, output.Total);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void NaoDeveCriarPedidoSeDimensoesProdutoInvalidas()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Items.Clear();
            payload.Items.Add(new() { IdProduto = 4, Quantidade = 1 });

            var output = Assert.Throws<Exception>(() => _checkout.Execute(payload));

            Assert.NotNull(output);
            Assert.Equal("Produto inválido.", output.Message);
        }

        [Trait("Cccat", "Application")]
        [Fact]
        public void NaoDeveCriarPedidoSePesoProdutoNegativo()
        {
            var payload = _checkoutFixture.CriarInputValidoSomenteItens();
            payload.Items.Clear();
            payload.Items.Add(new() { IdProduto = 5, Quantidade = 1 });

            var output = Assert.Throws<Exception>(() => _checkout.Execute(payload));

            Assert.NotNull(output);
            Assert.Equal("Produto inválido.", output.Message);
        }
    }
}
