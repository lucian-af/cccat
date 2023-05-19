using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Cccat.Infra;
using Cccat.Infra.Repositories;
using Cccat.UseCases.Models;

namespace Cccat.Tests.Fixtures
{
    public class SimulaFreteFixture
    {
        private readonly DatabaseContext _dbContext;

        public SimulaFreteFixture(DatabaseContext dbContext)
            => _dbContext = dbContext;

        public SimulaFreteInputDto CriarInputValido()
        {
            return new SimulaFreteInputDto
            {
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
    }
}
