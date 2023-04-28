using Cccat.Refactoring.DesignPatterns.ChainOfResponsibility;
using CoR = Cccat.Refactoring.DesignPatterns.ChainOfResponsibility;

namespace Cccat.Refactoring.Test.DesignPatterns.ChainOfResponsibility
{
    public class CorridaTest
    {
        private readonly CoR.Corrida _corrida;

        public CorridaTest()
        {
            var tarifaDomingoNoiteHanlder = new TarifaDomingoNoiteHandler(null);
            var tarifaNormalDomingoHanlder = new TarifaNormalDomingoHandler(tarifaDomingoNoiteHanlder);
            var tarifaNormalNoiteHanlder = new TarifaNormalNoiteHandler(tarifaNormalDomingoHanlder);
            var tarifaNormalHanlder = new TarifaNormalHandler(tarifaNormalNoiteHanlder);

            _corrida = new CoR.Corrida(tarifaNormalHanlder);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormal()
        {
            _corrida.AdicionarSegmento(10, new DateTime(2023, 04, 11, 16, 0, 0));

            var result = _corrida.CalcularTarifa();

            Assert.Equal(21, result);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormalDomingo()
        {
            _corrida.AdicionarSegmento(10, new DateTime(2023, 04, 09, 16, 0, 0));

            var result = _corrida.CalcularTarifa();

            Assert.Equal(29, result);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturnoExcetoDomingo()
        {
            _corrida.AdicionarSegmento(10, new DateTime(2023, 04, 11, 2, 0, 0));

            var result = _corrida.CalcularTarifa();

            Assert.Equal(39, result);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturnoDomingo()
        {
            _corrida.AdicionarSegmento(10, new DateTime(2023, 04, 9, 2, 0, 0));

            var result = _corrida.CalcularTarifa();

            Assert.Equal(50, result);
        }

        [Fact]
        public void DeveRetornarValorMinimoCorridaSeValorCalculadoMenorQueDez()
        {
            _corrida.AdicionarSegmento(1, new DateTime(2023, 04, 11, 12, 0, 0));

            var result = _corrida.CalcularTarifa();

            Assert.Equal(10, result);
        }

        [Fact]
        public void DeveCalcularMaisDeUmaCorrida()
        {
            _corrida.AdicionarSegmento(10, new DateTime(2023, 04, 11, 2, 0, 0));
            _corrida.AdicionarSegmento(10, new DateTime(2023, 04, 11, 12, 0, 0));
            _corrida.AdicionarSegmento(10, new DateTime(2023, 04, 9, 2, 0, 0));
            _corrida.AdicionarSegmento(10, new DateTime(2023, 04, 9, 12, 0, 0));
            _corrida.AdicionarSegmento(1, new DateTime(2023, 04, 9, 12, 0, 0));

            var result = _corrida.CalcularTarifa();

            Assert.Equal(141.9M, result);
        }
    }
}