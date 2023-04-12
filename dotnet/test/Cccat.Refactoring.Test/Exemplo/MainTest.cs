namespace Cccat.Refactoring.Test.Exemplo
{
    public class MainTest
    {
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormal()
        {
            var corridas = new List<Refactoring.Exemplo.Corrida>
            {
                new Refactoring.Exemplo.Corrida
                {
                    Dist = "10",
                    Ds = "2023-04-11 20:00:00"
                }
            };

            var result = Refactoring.Exemplo.Main.CalcularCorrida(corridas);

            Assert.Equal(21, result);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormalDomingo()
        {
            var corridas = new List<Refactoring.Exemplo.Corrida>
            {
                new Refactoring.Exemplo.Corrida
                {
                    Dist = "10",
                    Ds = "2023-04-09 20:00:00"
                }
            };

            var result = Refactoring.Exemplo.Main.CalcularCorrida(corridas);

            Assert.Equal(29, result);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturno()
        {
            var corridas = new List<Refactoring.Exemplo.Corrida>
            {
                new Refactoring.Exemplo.Corrida
                {
                    Dist = "10",
                    Ds = "2023-04-11 23:15:00"
                }
            };

            var result = Refactoring.Exemplo.Main.CalcularCorrida(corridas);

            Assert.Equal(39, result);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturnoDomingo()
        {
            var corridas = new List<Refactoring.Exemplo.Corrida>
            {
                new Refactoring.Exemplo.Corrida
                {
                    Dist = "10",
                    Ds = "2023-04-09 23:15:00"
                }
            };

            var result = Refactoring.Exemplo.Main.CalcularCorrida(corridas);

            Assert.Equal(50, result);
        }

        [Fact]
        public void DeveRetornarMenosDoisSeDataInvalida()
        {
            var corridas = new List<Refactoring.Exemplo.Corrida>
            {
                new Refactoring.Exemplo.Corrida
                {
                    Dist = "10",
                    Ds = "abc"
                }
            };

            var result = Refactoring.Exemplo.Main.CalcularCorrida(corridas);

            Assert.Equal(-2, result);
        }

        [Fact]
        public void DeveRetornarMenosUmSeDistanciaInvalida()
        {
            var corridas = new List<Refactoring.Exemplo.Corrida>
            {
                new Refactoring.Exemplo.Corrida
                {
                    Dist = "-10",
                    Ds = "2023-04-11 20:00:00"
                }
            };

            var result = Refactoring.Exemplo.Main.CalcularCorrida(corridas);

            Assert.Equal(-1, result);
        }

        [Fact]
        public void DeveRetornarValorMinimoCorridaSeValorCalculadoMenorQueDez()
        {
            var corridas = new List<Refactoring.Exemplo.Corrida>
            {
                new Refactoring.Exemplo.Corrida
                {
                    Dist = "1",
                    Ds = "2023-04-11 20:00:00"
                }
            };

            var result = Refactoring.Exemplo.Main.CalcularCorrida(corridas);

            Assert.Equal(10, result);
        }

        [Fact]
        public void DeveCalcularMaisDeUmaCorrida()
        {
            var corridas = new List<Refactoring.Exemplo.Corrida>
            {
                new Refactoring.Exemplo.Corrida
                {
                    Dist = "10",
                    Ds = "2023-04-11 20:00:00"
                },
                new Refactoring.Exemplo.Corrida
                {
                    Dist = "10",
                    Ds = "2023-04-10 20:00:00"
                }
            };

            var result = Refactoring.Exemplo.Main.CalcularCorrida(corridas);

            Assert.Equal(42, result);
        }
    }
}