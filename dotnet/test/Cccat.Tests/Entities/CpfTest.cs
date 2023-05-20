using Cccat.Entities;

namespace Cccat.Tests.Entities
{
    public class CpfTest
    {
        [Trait("Cccat", "Entities.Cpf")]
        [Fact]
        public void DeveCriarCpfValido()
        {
            var cpf = new Cpf("407.302.170.27");
            Assert.NotNull(cpf);
        }

        [Trait("Cccat", "Entities.Cpf")]
        [Fact]
        public void NaoDeveCriarCpfInvalido()
        {
            var result = Assert.Throws<Exception>(() => new Cpf("406.302.170.27"));

            Assert.NotNull(result);
            Assert.Equal("Cpf inválido.", result.Message);
        }
    }
}
