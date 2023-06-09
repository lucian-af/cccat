using Cccat.Refactoring.ObjetoEstudo;

namespace Cccat.Refactoring.Test.ObjetoEstudo
{
    public class MainTest
    {
        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormal()
        {
            var corridas = new List<LsParams>
            {
                new LsParams
                {
                    Dist = "10",
                    Ds = "2023-04-11 20:00:00"
                }
            };

            var result = Refactoring.ObjetoEstudo.Main.CalcularCorrida(corridas);

            Assert.Equal(21, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormalDomingo()
        {
            var corridas = new List<LsParams>
            {
                new LsParams
                {
                    Dist = "10",
                    Ds = "2023-04-09 20:00:00"
                }
            };

            var result = Refactoring.ObjetoEstudo.Main.CalcularCorrida(corridas);

            Assert.Equal(29, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturno()
        {
            var corridas = new List<LsParams>
            {
                new LsParams
                {
                    Dist = "10",
                    Ds = "2023-04-11 23:15:00"
                }
            };

            var result = Refactoring.ObjetoEstudo.Main.CalcularCorrida(corridas);

            Assert.Equal(39, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturnoDomingo()
        {
            var corridas = new List<LsParams>
            {
                new LsParams
                {
                    Dist = "10",
                    Ds = "2023-04-09 23:15:00"
                }
            };

            var result = Refactoring.ObjetoEstudo.Main.CalcularCorrida(corridas);

            Assert.Equal(50, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveRetornarMenosDoisSeDataInvalida()
        {
            var corridas = new List<LsParams>
            {
                new LsParams
                {
                    Dist = "10",
                    Ds = "abc"
                }
            };

            var result = Refactoring.ObjetoEstudo.Main.CalcularCorrida(corridas);

            Assert.Equal(-2, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveRetornarMenosUmSeDistanciaInvalida()
        {
            var corridas = new List<LsParams>
            {
                new LsParams
                {
                    Dist = "-10",
                    Ds = "2023-04-11 20:00:00"
                }
            };

            var result = Refactoring.ObjetoEstudo.Main.CalcularCorrida(corridas);

            Assert.Equal(-1, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveRetornarValorMinimoCorridaSeValorCalculadoMenorQueDez()
        {
            var corridas = new List<LsParams>
            {
                new LsParams
                {
                    Dist = "1",
                    Ds = "2023-04-11 20:00:00"
                }
            };

            var result = Refactoring.ObjetoEstudo.Main.CalcularCorrida(corridas);

            Assert.Equal(10, result);
        }

        [Trait("Cccat", "Refactoring")]
        [Fact]
        public void DeveCalcularMaisDeUmaCorrida()
        {
            var corridas = new List<LsParams>
            {
                new LsParams
                {
                    Dist = "10",
                    Ds = "2023-04-11 20:00:00"
                },
                new LsParams
                {
                    Dist = "10",
                    Ds = "2023-04-10 20:00:00"
                }
            };

            var result = Refactoring.ObjetoEstudo.Main.CalcularCorrida(corridas);

            Assert.Equal(42, result);
        }
    }
}