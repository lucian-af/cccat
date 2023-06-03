using Cccat.Checkout.Application.UseCase;
using Cccat.Checkout.Tests.Fixtures;

namespace Cccat.Checkout.Tests.UseCases
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

        [Trait("Cccat", "UseCases.Checkout.ConsultaProduto")]
        [Fact]
        public void DeveObterTodosProdutos()
        {
            var output = _consultaProduto.ObterTodos();

            Assert.NotNull(output);
            Assert.True(output.Any());
        }
    }
}
