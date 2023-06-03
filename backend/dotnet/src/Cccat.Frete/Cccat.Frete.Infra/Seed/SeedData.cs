using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Infra.Database;

namespace Cccat.Frete.Infra.Seed
{
    public static class SeedData
    {
        public static async Task CriarDados(DatabaseContext context)
        {
            await AdicionarProdutos(context);

            await AdicionarCupons(context);

            await AdicionarCeps(context);

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
                //new Produto(4,"D",30,-1,-1,-1,1),
                //new Produto(5,"E",30,1,1,1,-1)
            };

            await context.Produtos.AddRangeAsync(produtos);
        }

        private static async Task AdicionarCupons(DatabaseContext context)
        {
            if (context.Cupons.Any()) return;

            var cupons = new List<Cupom>
            {
                new Cupom(1, "VALE20", 20, DateTime.Today.AddDays(20)),
                new Cupom(2, "VALE50", 50, DateTime.Today.AddDays(-10))
            };

            await context.Cupons.AddRangeAsync(cupons);
        }

        private static async Task AdicionarCeps(DatabaseContext context)
        {
            if (context.Ceps.Any()) return;

            var ceps = new List<Cep>
            {
                new Cep("17600090","Rua Cherentes","Centro",-21.940867,-50.506929),
                new Cep("72980000","Rua Manoel","Vila Pratinha",-15.855776,-48.955921)
            };

            await context.Ceps.AddRangeAsync(ceps);
        }
    }
}
