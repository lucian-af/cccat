using Cccat.Catalogo.Application.UseCase;
using Cccat.Catalogo.Tests.Fixtures;

namespace Cccat.Catalogo.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class ConsultaProdutoTest
    {
        private readonly ConsultaProduto _consultaProduto;

        public ConsultaProdutoTest(DatabaseFixture dbFixture)
        {
            var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
                .CriarRepositoryFactory();
            _consultaProduto = new ConsultaProduto(factory);
        }

        [Trait("Cccat", "UseCases.Catalogo.ConsultaProduto")]
        [Fact]
        public void DeveObterProdutoPorId()
        {
            var output = _consultaProduto.Buscar(1);

            Assert.NotNull(output);
            Assert.Equal(1, output.IdProduto);
            Assert.Equal("A", output.Descricao);
            Assert.Equal(1000, output.Preco);
            Assert.Equal(100, output.Largura);
            Assert.Equal(30, output.Altura);
            Assert.Equal(10, output.Profundidade);
            Assert.Equal(3, output.Peso);
            Assert.Equal(0.03m, output.Volume);
            Assert.Equal(100, output.Densidade);
        }
    }
}
