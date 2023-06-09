using Cccat.Checkout.Domain.Entities;
using Cccat.Checkout.Infra.Database;

namespace Cccat.Checkout.Infra.Seed
{
    public static class SeedData
    {
        public static async Task CriarDados(DatabaseContext context)
        {
            await AdicionarCupons(context);

            await AdicionarCeps(context);

            await context.SaveChangesAsync();
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
