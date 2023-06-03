using Cccat.Catalogo.Domain.Entities;
using Cccat.Catalogo.Infra.Database;

namespace Cccat.Catalogo.Infra.Seed
{
    public static class SeedData
    {
        public static async Task CriarDados(DatabaseContext context)
        {
            await AdicionarProdutos(context);

            await context.SaveChangesAsync();
        }

        private static async Task AdicionarProdutos(DatabaseContext context)
        {
            if (context.Produtos.Any()) return;

            var produtos = new List<Produto>
            {
                new Produto(1,"A",1000,100,30,10,3),
                new Produto(2,"B",5000,50,50,50,22),
                new Produto(3,"C",30,10,10,10,.9m)
            };

            await context.Produtos.AddRangeAsync(produtos);
        }
    }
}
