using Cccat.Domain.Entities;

namespace Cccat.Tests.Entities
{
    public class CpfTest
    {
        [Trait("Cccat", "Entities.Cpf")]
        [Theory]
        [InlineData("407.302.170-27")]
        [InlineData("684.053.160-00")]
        [InlineData("746.971.314-01")]
        public void DeveCriarCpfValido(string input)
        {
            var cpf = new Cpf(input);
            Assert.NotNull(cpf);
        }

        [Trait("Cccat", "Entities.Cpf")]
        [Theory]
        [InlineData("406.302.170-27")]
        [InlineData("684.053.160")]
        [InlineData("746.971")]
        [InlineData("746")]
        [InlineData("1q2w3e4r")]
        [InlineData("111.111.111-11")]
        [InlineData("222.222.222-22")]
        [InlineData("333.333.333-33")]
        [InlineData("444.444.444-44")]
        [InlineData("555.555.555-55")]
        [InlineData("666.666.666-66")]
        [InlineData("777.777.777-77")]
        [InlineData("888.888.888-88")]
        [InlineData("999.999.999-99")]
        [InlineData("000.000.000-00")]
        [InlineData(" ")]
        [InlineData(null)]
        public void NaoDeveCriarCpfInvalido(string input)
        {
            var result = Assert.Throws<Exception>(() => new Cpf(input));

            Assert.NotNull(result);
            Assert.Equal("Cpf inválido.", result.Message);
        }
    }
}
