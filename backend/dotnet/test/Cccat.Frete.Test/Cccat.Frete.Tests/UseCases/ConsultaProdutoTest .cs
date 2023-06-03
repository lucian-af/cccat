using Cccat.Application.UseCase;
using Cccat.Tests.Fixtures;

namespace Cccat.Tests.UseCases
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

        [Trait("Cccat", "UseCases.ConsultaProduto")]
        [Fact]
        public void DeveObterTodosProdutos()
        {
            var output = _consultaProduto.ObterTodos();

            Assert.NotNull(output);
            Assert.True(output.Any());
        }
    }
}
