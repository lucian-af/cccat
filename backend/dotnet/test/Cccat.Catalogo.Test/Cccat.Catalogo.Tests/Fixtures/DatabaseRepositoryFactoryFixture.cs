using Cccat.Catalogo.Domain.Entities;
using Cccat.Catalogo.Domain.Interfaces;
using Cccat.Catalogo.Infra.Database;
using Cccat.Catalogo.Infra.Factories;

namespace Cccat.Catalogo.Tests.Fixtures
{
    public class DatabaseRepositoryFactoryFixture
    {
        public readonly DatabaseContext DbContext;

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

        internal class DatabaseRepositoryFactoryFake : IRepositoryFactory
        {
            public IProdutoRepository CriarProdutoRepository()
                => new ProdutoRepositoryFake();
        }

        internal class ProdutoRepositoryFake : IProdutoRepository
        {
            private static List<Produto> Produtos()
                => new()
                {
                    new Produto(1,"A",1000,100,30,10,3),
                    new Produto(2,"B",5000,50,50,50,22),
                    new Produto(3,"C",30,10,10,10,.9m)
                };

            public Produto Get(int idProduto)
                => Produtos().Find(produto => produto.Id == idProduto);

            public IEnumerable<Produto> All()
                => Produtos();
        }
    }
}
