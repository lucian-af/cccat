namespace Cccat.Refactoring.Test
{
    public class MainTest
    {
        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormal()
        {
            var corridas = new List<Corrida>
            {
                new Corrida
                {
                    Distancia = "10",
                    DataHora = "2023-04-11 20:00:00"
                }
            };

            var result = Main.CalcularCorrida(corridas);

            Assert.Equal(21, result);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNormalDomingo()
        {
            var corridas = new List<Corrida>
            {
                new Corrida
                {
                    Distancia = "10",
                    DataHora = "2023-04-09 20:00:00"
                }
            };

            var result = Main.CalcularCorrida(corridas);

            Assert.Equal(29, result);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturno()
        {
            var corridas = new List<Corrida>
            {
                new Corrida
                {
                    Distancia = "10",
                    DataHora = "2023-04-11 23:15:00"
                }
            };

            var result = Main.CalcularCorrida(corridas);

            Assert.Equal(39, result);
        }

        [Fact]
        public void DeveCalulcarValorCorridaHorarioNoturnoDomingo()
        {
            var corridas = new List<Corrida>
            {
                new Corrida
                {
                    Distancia = "10",
                    DataHora = "2023-04-09 23:15:00"
                }
            };

            var result = Main.CalcularCorrida(corridas);

            Assert.Equal(50, result);
        }

        [Fact]
        public void DeveRetornarMenosDoisSeDataInvalida()
        {
            var corridas = new List<Corrida>
            {
                new Corrida
                {
                    Distancia = "10",
                    DataHora = "abc"
                }
            };

            var exception = Assert.Throws<ArgumentException>(() => Main.CalcularCorrida(corridas));

            Assert.Equal("DataHora inválida.", exception.Message);
        }

        [Fact]
        public void DeveRetornarMenosUmSeDistanciaanciaInvalida()
        {
            var corridas = new List<Corrida>
            {
                new Corrida
                {
                    Distancia = "-10",
                    DataHora = "2023-04-11 20:00:00"
                }
            };

            var exception = Assert.Throws<ArgumentException>(() => Main.CalcularCorrida(corridas));

            Assert.Equal("Distância inválida.", exception.Message);
        }

        [Fact]
        public void DeveRetornarValorMinimoCorridaSeValorCalculadoMenorQueDez()
        {
            var corridas = new List<Corrida>
            {
                new Corrida
                {
                    Distancia = "1",
                    DataHora = "2023-04-11 20:00:00"
                }
            };

            var result = Main.CalcularCorrida(corridas);

            Assert.Equal(10, result);
        }

        [Fact]
        public void DeveCalcularMaisDeUmaCorrida()
        {
            var corridas = new List<Corrida>
            {
                new Corrida
                {
                    Distancia = "10",
                    DataHora = "2023-04-11 20:00:00"
                },
                new Corrida
                {
                    Distancia = "10",
                    DataHora = "2023-04-10 20:00:00"
                }
            };

            var result = Main.CalcularCorrida(corridas);

            Assert.Equal(42, result);
        }
    }
}