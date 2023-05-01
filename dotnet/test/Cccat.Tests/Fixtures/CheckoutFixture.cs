using Cccat.Application;
using Cccat.Domain.Entities;
using Cccat.Domain.Interfaces;
using Cccat.Infra;
using Cccat.Infra.Repositories;
using Cccat.Infra.Seed;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Tests.Fixtures
{
    [CollectionDefinition(nameof(CheckoutFixtureCollection))]
    public class CheckoutFixtureCollection : ICollectionFixture<CheckoutFixture> { }

    public class CheckoutFixture
    {
        private DatabaseContext _dbContext;

        public Input CriarInputValido()
        {
            // TODO: usar Fakers
            return new Input
            {
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

        public Input CriarInputValidoSemCupomDesconto()
        {
            // TODO: usar Fakers
            return new Input
            {
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

        public Input CriarInputValidoSemFrete()
        {
            // TODO: usar Fakers
            return new Input
            {
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

        public Input CriarInputValidoSomenteItens()
        {
            // TODO: usar Fakers
            return new Input
            {
                Cpf = "407.302.170-27",
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                }
            };
        }

        public Input CriarInputValidoComCupom()
        {
            // TODO: usar Fakers
            return new Input
            {
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

        public Input CriarInputValidoComFrete()
        {
            // TODO: usar Fakers
            return new Input
            {
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

        public void CriarDatabaseInMemory()
        {
            var dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseInMemoryDatabase("CCCAT");
            _dbContext = new DatabaseContext(dbOptions.Options);
            SeedData.CriarDados(_dbContext).Wait();
        }
    }

    internal class ProdutoRepositoryFake : IProdutoRepository
    {
        private static List<Produto> Produtos()
            => new()
            {
                new Produto
                {
                    Id = 1,
                    Descricao = "A",
                    Preco = 1000,
                    Largura = 100,
                    Altura = 30,
                    Profundidade = 10,
                    Peso = 3
                },
                new Produto
                {
                    Id = 2,
                    Descricao = "B",
                    Preco = 5000,
                    Largura = 50,
                    Altura = 50,
                    Profundidade = 50,
                    Peso = 22
                },
                new Produto
                {
                    Id = 3,
                    Descricao = "C",
                    Preco = 30,
                    Largura = 10,
                    Altura = 10,
                    Profundidade = 10,
                    Peso = 0.9M
                },
                new Produto
                {
                    Id = 4,
                    Descricao = "C",
                    Preco = 30,
                    Largura = -1,
                    Altura = -1,
                    Profundidade = -1,
                    Peso = 1M
                },
                new Produto
                {
                    Id = 5,
                    Descricao = "C",
                    Preco = 30,
                    Largura = 1,
                    Altura = 1,
                    Profundidade = 1,
                    Peso = -1M
                }
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
}
