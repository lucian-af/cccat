using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Cccat.Tests.Fixtures;
using Cccat.UseCases;

namespace Cccat.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class ConsultaPedidoTest
    {
        private readonly ConsultaPedido _consultaPedido;
        private IPedidoRepository _pedidoRepository;
        private readonly ConsultaPedidoFixture _consultaPedidoFixture;

        public ConsultaPedidoTest(DatabaseFixture dbFixture)
        {
            _consultaPedidoFixture = new(dbFixture.DbContext);
            _pedidoRepository = _consultaPedidoFixture.CriarPedidoRepository(false);
            _consultaPedido = new(_pedidoRepository);
        }

        [Trait("Cccat", "UseCases.ConsultaPedido")]
        [Fact]
        public void DeveConsultarPedidoPorId()
        {
            var idPedido = Guid.NewGuid();
            var pedido = new Pedido(idPedido, "407.302.170.27");
            _pedidoRepository.AdicionarPedido(pedido);

            var result = _consultaPedido.ConsultaPorId(idPedido);

            Assert.NotNull(result);
            Assert.Equal(idPedido, result.Id);
        }

        [Trait("Cccat", "UseCases.ConsultaPedido")]
        [Fact]
        public void DeveConsultarPedidoPorCodigo()
        {
            var pedido = new Pedido(Guid.NewGuid(), "407.302.170.27");
            _pedidoRepository.AdicionarPedido(pedido);

            var result = _consultaPedido.ConsultaPorCodigo(pedido.Codigo);

            Assert.NotNull(result);
            Assert.Equal(pedido.Codigo, result.Codigo);
        }

        [Trait("Cccat", "UseCases.ConsultaPedido")]
        [Fact]
        public void DeveConsultarTodosPedidos()
        {
            _pedidoRepository.AdicionarPedido(new Pedido(Guid.NewGuid(), "407.302.170.27"));
            _pedidoRepository.AdicionarPedido(new Pedido(Guid.NewGuid(), "407.302.170.27"));

            var result = _consultaPedido.ConsultaTodos();

            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
        }
    }
}
