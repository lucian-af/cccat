using Cccat.Frete.Domain.Entities;

namespace Cccat.Frete.Tests.Entities
{
    public class PedidoTest
    {
        [Trait("Cccat", "Entities.Frete.Pedido")]
        [Fact]
        public void NaoDeveCriarPedidoComCpfInvalido()
        {
            var result = Assert.Throws<Exception>(() => new Pedido(Guid.NewGuid(), "467.302.170.27"));

            Assert.NotNull(result);
            Assert.Equal("Cpf inválido.", result.Message);
        }

        [Trait("Cccat", "Entities.Frete.Pedido")]
        [Fact]
        public void DeveCriarPedidoVazio()
        {
            var pedido = new Pedido(Guid.NewGuid(), "407.302.170.27");

            Assert.NotNull(pedido);
            Assert.Equal(0, pedido.SubTotal);
        }

        [Trait("Cccat", "Entities.Frete.Pedido")]
        [Fact]
        public void DeveCriarPedidoCom3Itens()
        {
            var pedido = new Pedido(Guid.NewGuid(), "407.302.170.27");
            pedido.AdicionarItem(1, 1000, 1);
            pedido.AdicionarItem(2, 5000, 1);
            pedido.AdicionarItem(3, 30, 3);

            Assert.Equal(6090, pedido.SubTotal);
        }

        [Trait("Cccat", "Entities.Frete.Pedido")]
        [Fact]
        public void NaoDeveCriarPedidoComItemDuplicado()
        {
            var pedido = new Pedido(Guid.NewGuid(), "407.302.170.27");
            pedido.AdicionarItem(1, 1000, 1);

            var result = Assert.Throws<Exception>(() => pedido.AdicionarItem(1, 1000, 1));

            Assert.NotNull(result);
            Assert.Equal("Não é permitido duplicar o mesmo item.", result.Message);
        }

        [Trait("Cccat", "Entities.Frete.Pedido")]
        [Fact]
        public void DeveCriarPedidoEGeraCodigo()
        {
            var pedido = new Pedido(Guid.NewGuid(), "407.302.170.27");

            Assert.NotNull(pedido);
            Assert.Equal($"{DateTime.Now.Year}00000001", pedido.Codigo);
        }

        [Trait("Cccat", "Entities.Frete.Pedido")]
        [Fact]
        public void DeveCriarPedidoEAdicionarFreteValido()
        {
            var pedido = new Pedido(Guid.NewGuid(), "407.302.170.27");

            var result = Assert.Throws<Exception>(() => pedido.AdicionarFrete(-1));

            Assert.NotNull(result);
            Assert.Equal("Valor do frete deve ser maior que zero.", result.Message);
        }
    }
}
