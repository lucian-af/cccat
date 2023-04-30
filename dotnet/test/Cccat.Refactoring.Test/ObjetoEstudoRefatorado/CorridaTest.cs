using Cccat.Refactoring.ObjetoEstudoRefatorado;

namespace Cccat.Refactoring.Test.ObjetoEstudoRefatorado
{
    public class CorridaTest
    {
        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormal()
        {
            var corrida = new Corrida();
            corrida.AdicionarSegmento(10, new DateTime(2023, 04, 11, 16, 0, 0));

            var result = corrida.CalcularTarifa();

            Assert.Equal(21, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormalDomingo()
        {
            var corrida = new Corrida();
            corrida.AdicionarSegmento(10, new DateTime(2023, 04, 09, 16, 0, 0));

            var result = corrida.CalcularTarifa();

            Assert.Equal(29, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturnoExcetoDomingo()
        {
            var corrida = new Corrida();
            corrida.AdicionarSegmento(10, new DateTime(2023, 04, 11, 2, 0, 0));

            var result = corrida.CalcularTarifa();

            Assert.Equal(39, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturnoDomingo()
        {
            var corrida = new Corrida();
            corrida.AdicionarSegmento(10, new DateTime(2023, 04, 9, 2, 0, 0));

            var result = corrida.CalcularTarifa();

            Assert.Equal(50, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveRetornarValorMinimoCorridaSeValorCalculadoMenorQueDez()
        {
            var corrida = new Corrida();
            corrida.AdicionarSegmento(1, new DateTime(2023, 04, 11, 12, 0, 0));

            var result = corrida.CalcularTarifa();

            Assert.Equal(10, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalcularMaisDeUmaCorrida()
        {
            var corrida = new Corrida();
            corrida.AdicionarSegmento(10, new DateTime(2023, 04, 11, 2, 0, 0));
            corrida.AdicionarSegmento(10, new DateTime(2023, 04, 11, 12, 0, 0));
            corrida.AdicionarSegmento(10, new DateTime(2023, 04, 9, 2, 0, 0));
            corrida.AdicionarSegmento(10, new DateTime(2023, 04, 9, 12, 0, 0));
            corrida.AdicionarSegmento(1, new DateTime(2023, 04, 9, 12, 0, 0));

            var result = corrida.CalcularTarifa();

            Assert.Equal(141.9M, result);
        }
    }
}