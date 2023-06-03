using Cccat.Catalogo.Application.UseCase;
using Cccat.Catalogo.Tests.Fixtures;

namespace Cccat.Catalogo.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class ConsultaProdutosTest
    {
        private readonly ConsultaProdutos _consultaProdutos;

        public ConsultaProdutosTest(DatabaseFixture dbFixture)
        {
            var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
                .CriarRepositoryFactory();
            _consultaProdutos = new ConsultaProdutos(factory);
        }

        [Trait("Cccat", "UseCases.Catalogo.ConsultaProdutos")]
        [Fact]
        public void DeveObterTodosProdutos()
        {
            var output = _consultaProdutos.Buscar();

            Assert.NotNull(output);
            Assert.True(output.Any());
        }
    }
}
