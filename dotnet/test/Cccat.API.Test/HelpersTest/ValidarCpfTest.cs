using Cccat.API.Helpers;

namespace Cccat.API.Test.HelpersTest
{
    public class ValidarCpfTest
    {
        [Theory]
        [InlineData("407.302.170-27")]
        [InlineData("684.053.160-00")]
        [InlineData("746.971.314-01")]
        public void DeveRetornarVerdadeiroSeCPFValido(string cpf)
        {
            var result = ValidarCpf.Validar(cpf);

            Assert.True(result);
        }

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
        public void DeveRetornarFalsoSeCPFInvalido(string cpf)
        {
            var result = ValidarCpf.Validar(cpf);

            Assert.False(result);
        }
    }
}