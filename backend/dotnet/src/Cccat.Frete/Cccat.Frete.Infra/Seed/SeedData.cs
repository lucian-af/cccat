using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Infra.Database;

namespace Cccat.Frete.Infra.Seed
{
    public static class SeedData
    {
        public static async Task CriarDados(DatabaseContext context)
        {
            await AdicionarCeps(context);

            await context.SaveChangesAsync();
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
