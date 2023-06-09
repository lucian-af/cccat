using Cccat.Checkout.Domain.Entities;
using Cccat.Checkout.Infra.Database;

namespace Cccat.Checkout.Infra.Seed
{
	public static class SeedData
	{
		public static async Task CriarDados(DatabaseContext context)
		{
			await AdicionarCupons(context);

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
	}
}
