using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Cccat.Infra;
using Cccat.Infra.Repositories;
using Cccat.UseCases.Models;

namespace Cccat.Tests.Fixtures
{
    public class CheckoutFixture
    {
        private readonly DatabaseContext _dbContext;

        public CheckoutFixture(DatabaseContext dbContext)
            => _dbContext = dbContext;

        public CheckoutInputDto CriarInputValido()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
                Cpf = "407.302.170-27",
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                },
                Cupom = "VALE20",
                CepOrigem = "17600090",
                CepDestino = "17602700"
            };
        }

        public CheckoutInputDto CriarInputValidoSemCupomDesconto()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
                Cpf = "407.302.170-27",
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                },
                CepOrigem = "17600090",
                CepDestino = "17602700"
            };
        }

        public CheckoutInputDto CriarInputValidoSemFrete()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
                Cpf = "407.302.170-27",
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                },
                Cupom = "VALE20"
            };
        }

        public CheckoutInputDto CriarInputValidoSomenteItens()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
                Cpf = "407.302.170-27",
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                }
            };
        }

        public CheckoutInputDto CriarInputValidoComCupom()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
                Cpf = "407.302.170-27",
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                },
                Cupom = "VALE20"
            };
        }

        public CheckoutInputDto CriarInputValidoComFrete()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
                Cpf = "407.302.170-27",
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                },
                CepOrigem = "17600090",
                CepDestino = "17602700"
            };
        }

        public IProdutoRepository CriarProdutoRepository(bool fake = true)
        {
            if (fake)
                return new ProdutoRepositoryFake();

            return new ProdutoRepository(_dbContext);
        }

        public ICupomRepository CriarCupomRepository(bool fake = true)
        {
            if (fake)
                return new CupomRepositoryFake();

            return new CupomRepository(_dbContext);
        }

        public IPedidoRepository CriarPedidoRepository(bool fake = true)
        {
            if (fake)
                return new PedidoRepositoryFake();

            return new PedidoRepository(_dbContext);
        }

        public void DeletarTodosPedidos()
            => _dbContext.RemoveRange(_dbContext.Pedidos);

        internal class ProdutoRepositoryFake : IProdutoRepository
        {
            private static List<Produto> Produtos()
                => new()
                {
                    new Produto(1,"A",1000,100,30,10,3),
                    new Produto(2,"B",5000,50,50,50,22),
                    new Produto(3,"C",30,10,10,10,.9m),
                    new Produto(4,"D",30,-1,-1,-1,1),
                    new Produto(5,"E",30,1,1,1,-1)
                };

            public Produto Get(int idProduto)
                => Produtos().Find(produto => produto.Id == idProduto);
        }

        internal class CupomRepositoryFake : ICupomRepository
        {
            private static List<Cupom> Cupons() => new()
        {
            new Cupom
                {
                    Id = 1,
                    Codigo = "VALE20",
                    Percentual = 20,
                    Validade = DateTime.Today.AddDays(20)
                },
                new Cupom
                {
                    Id = 2,
                    Codigo = "VALE50",
                    Percentual = 50,
                    Validade = DateTime.Today.AddDays(-10)
                },
        };

            public Cupom Get(string codigo)
                => Cupons().Find(cupom => cupom.Codigo.Equals(codigo));
        }

        internal class PedidoRepositoryFake : IPedidoRepository
        {
            public Pedido ConsultarPedidoPorId(Guid idPedido)
                => new()
                {
                    Id = idPedido,
                    Cpf = "407.302.170-27",
                    Frete = 0,
                    Total = 6090
                };

            public Task AdicionarPedido(Pedido pedido)
            {
                Console.WriteLine("Fake.");
                return Task.CompletedTask;
            }

            public async Task<long> ObterTotalPedidos()
                => await Task.FromResult(0);
        }
    }
}
