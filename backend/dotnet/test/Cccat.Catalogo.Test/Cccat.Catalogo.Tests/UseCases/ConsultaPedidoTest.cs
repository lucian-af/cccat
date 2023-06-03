﻿using Cccat.Catalogo.Application.UseCase;
using Cccat.Catalogo.Domain.Entities;
using Cccat.Catalogo.Domain.Interfaces;
using Cccat.Catalogo.Tests.Fixtures;

namespace Cccat.Catalogo.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class ConsultaPedidoTest
    {
        private readonly ConsultaPedido _consultaPedido;
        private readonly IPedidoRepository _pedidoRepository;

        public ConsultaPedidoTest(DatabaseFixture dbFixture)
        {
            var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
                .CriarRepositoryFactory();
            _pedidoRepository = factory.CriarPedidoRepository();
            _consultaPedido = new(factory);
        }

        [Trait("Cccat", "UseCases.Catalogo.ConsultaPedido")]
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

        [Trait("Cccat", "UseCases.Catalogo.ConsultaPedido")]
        [Fact]
        public void DeveConsultarPedidoPorCodigo()
        {
            var pedido = new Pedido(Guid.NewGuid(), "407.302.170.27");
            _pedidoRepository.AdicionarPedido(pedido);

            var result = _consultaPedido.ConsultaPorCodigo(pedido.Codigo);

            Assert.NotNull(result);
            Assert.Equal(pedido.Codigo, result.Codigo);
        }

        [Trait("Cccat", "UseCases.Catalogo.ConsultaPedido")]
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
