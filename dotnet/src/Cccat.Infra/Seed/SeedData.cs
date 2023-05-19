using Cccat.Entities;

namespace Cccat.Infra.Seed
{
    public static class SeedData
    {
        public static async Task CriarDados(DatabaseContext context)
        {
            await AdicionarProdutos(context);

            await AdicionarCupons(context);

            await context.SaveChangesAsync();
        }

        private static async Task AdicionarProdutos(DatabaseContext context)
        {
            if (context.Produtos.Any()) return;

            var produtos = new List<Produto>
            {
                new Produto(1,"A",1000,100,30,10,3),
                new Produto(2,"B",5000,50,50,50,22),
                new Produto(3,"C",30,10,10,10,.9m),
                new Produto(4,"D",30,-1,-1,-1,1),
                new Produto(5,"E",30,1,1,1,-1)
            };

            await context.Produtos.AddRangeAsync(produtos);
        }

        private static async Task AdicionarCupons(DatabaseContext context)
        {
            if (context.Cupons.Any()) return;

            var cupons = new List<Cupom>
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

            await context.Cupons.AddRangeAsync(cupons);
        }
    }
}
