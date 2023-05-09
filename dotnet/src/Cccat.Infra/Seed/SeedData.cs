using Cccat.Entities.Negocio;

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
