using Cccat.Application;

namespace Cccat.Tests.Fixtures
{
    [CollectionDefinition(nameof(CheckoutFixtureCollection))]
    public class CheckoutFixtureCollection : ICollectionFixture<CheckoutFixture> { }

    public class CheckoutFixture
    {
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
    }
}
