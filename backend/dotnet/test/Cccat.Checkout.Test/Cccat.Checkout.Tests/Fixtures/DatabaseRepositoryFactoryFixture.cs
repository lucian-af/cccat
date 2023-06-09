using Cccat.Checkout.Domain.Entities;
using Cccat.Checkout.Domain.Interfaces;
using Cccat.Checkout.Infra.Database;
using Cccat.Checkout.Infra.Factories;

namespace Cccat.Checkout.Tests.Fixtures
{
    public class DatabaseRepositoryFactoryFixture
    {
        public readonly DatabaseContext DbContext;
        internal static List<Pedido> Pedidos = new();

        private bool Fake { get; set; } = false;

        public DatabaseRepositoryFactoryFixture() { }

        public DatabaseRepositoryFactoryFixture(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public IRepositoryFactory CriarRepositoryFactory()
        {
            if (Fake)
                return new DatabaseRepositoryFactoryFake();

            return new RepositoryFactory(DbContext);
        }

        public void DeletarTodosPedidos()
        {
            if (Fake)
                Pedidos = new();
            else
                DbContext.RemoveRange(DbContext.Pedidos);
        }

        internal class DatabaseRepositoryFactoryFake : IRepositoryFactory
        {
            public ICepRepository CriarCepRepository()
                => new CepRepositoryFake();

            public ICupomRepository CriarCupomRepository()
                => new CupomRepositoryFake();

            public IPedidoRepository CriarPedidoRepository()
                => new PedidoRepositoryFake();
        }

        internal class CepRepositoryFake : ICepRepository
        {
            private static List<Cep> Ceps()
                => new()
                {
                    new Cep("17600090","Rua Cherentes","Centro",-21.940867,-50.506929),
                    new Cep("72980000","Rua Manoel","Vila Pratinha",-15.855776,-48.955921)
                };

            public void AdicionarCep(Cep cep)
                => throw new NotImplementedException();

            public Cep Get(string codigo)
                => Ceps().Find(cep => cep.Codigo.Equals(codigo));
        }

        internal class CupomRepositoryFake : ICupomRepository
        {
            private static List<Cupom> Cupons() => new()
            {
                new Cupom(1, "VALE20", 20, DateTime.Today.AddDays(20)),
                new Cupom(2, "VALE50", 50, DateTime.Today.AddDays(-10))
            };

            public Cupom Get(string codigo)
                => Cupons().Find(cupom => cupom.Codigo.Equals(codigo));
        }

        internal class PedidoRepositoryFake : IPedidoRepository
        {
            public Pedido ConsultarPedidoPorId(Guid idPedido)
                => Pedidos.Find(pedido => pedido.Id.Equals(idPedido));

            public Task AdicionarPedido(Pedido pedido)
            {
                Pedidos.Add(pedido);
                return Task.CompletedTask;
            }

            public async Task<long> ObterTotalPedidos()
                => await Task.FromResult(Pedidos.Count);

            public Pedido ConsultarPedidoPorCodigo(string codigo)
                => Pedidos.FirstOrDefault(pedido => pedido.Codigo.Equals(codigo));

            public IEnumerable<Pedido> ConsultaTodos()
                => Pedidos;
        }
    }
}
