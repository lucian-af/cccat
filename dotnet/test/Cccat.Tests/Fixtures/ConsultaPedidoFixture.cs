using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Cccat.Infra;
using Cccat.Infra.Repositories;

namespace Cccat.Tests.Fixtures
{
    public class ConsultaPedidoFixture
    {
        private readonly DatabaseContext _dbContext;

        public ConsultaPedidoFixture(DatabaseContext dbContext)
            => _dbContext = dbContext;

        public IPedidoRepository CriarPedidoRepository(bool fake = true)
        {
            if (fake)
                return new PedidoRepositoryFake();

            return new PedidoRepository(_dbContext);
        }

        internal class PedidoRepositoryFake : IPedidoRepository
        {
            private List<Pedido> _pedidos = new();

            public Pedido ConsultarPedidoPorId(Guid idPedido)
                => _pedidos.Find(pedido => pedido.Id.Equals(idPedido));

            public Task AdicionarPedido(Pedido pedido)
            {
                _pedidos.Add(pedido);
                return Task.CompletedTask;
            }

            public async Task<long> ObterTotalPedidos()
                => await Task.FromResult(_pedidos.Count);

            public Pedido ConsultarPedidoPorCodigo(string codigo)
                => _pedidos.FirstOrDefault(pedido => pedido.Codigo.Equals(codigo));

            public IEnumerable<Pedido> ConsultaTodos()
                => _pedidos;
        }
    }
}
