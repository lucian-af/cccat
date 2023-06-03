using Cccat.Checkout.Domain.Entities;

namespace Cccat.Checkout.Tests.Entities
{
    public class CupomTest
    {
        [Trait("Cccat", "Entities.Checkout.Cupom")]
        [Fact]
        public void DeveValidarSeCupomValido()
        {
            var cupom = new Cupom(1, "VALE20", 20, DateTime.Today.AddMonths(1));

            Assert.True(cupom.Valido(DateTime.Today.AddDays(10)));
        }

        [Trait("Cccat", "Entities.Checkout.Cupom")]
        [Fact]
        public void DeveValidarSeCupomExpirado()
        {
            var cupom = new Cupom(1, "VALE50", 50, DateTime.Today.AddMonths(-2));

            Assert.False(cupom.Valido(DateTime.Today));
        }

        [Trait("Cccat", "Entities.Checkout.Cupom")]
        [Fact]
        public void DeveCalcularDesconto()
        {
            var cupom = new Cupom(1, "VALE20", 20, DateTime.Today.AddMonths(1));

            var desconto = cupom.CalcularDesconto(1000);

            Assert.Equal(200, desconto);
        }
    }
}
