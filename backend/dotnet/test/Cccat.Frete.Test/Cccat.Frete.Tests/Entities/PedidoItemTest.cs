using Cccat.Frete.Domain.Entities;

namespace Cccat.Frete.Tests.Entities
{
    public class PedidoItemTest
    {
        [Trait("Cccat", "Entities.Frete.PedidoItem")]
        [Fact]
        public void NaoDeveCriarItemComQuantidadeInvalida()
        {
            var result = Assert.Throws<Exception>(() => new PedidoItem(Guid.NewGuid(), 1, 1000, 0));

            Assert.NotNull(result);
            Assert.Equal("Quantidade inválida.", result.Message);
        }
    }
}
