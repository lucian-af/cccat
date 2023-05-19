﻿using Cccat.UseCases.Models;

namespace Cccat.API.Test.Fixtures
{
    [CollectionDefinition(nameof(PedidoFixtureCollection))]
    public class PedidoFixtureCollection : ICollectionFixture<PedidoFixture> { }

    public class PedidoFixture
    {
        public readonly HttpClient Client;
        private readonly CustomWebApiFactory<Program> _factory;

        public PedidoFixture()
        {
            _factory = new CustomWebApiFactory<Program>();
            Client = _factory.CreateClient();
        }

        public CheckoutInputDto CriarInputValido()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
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

        public CheckoutInputDto CriarInputValidoSemCupomDesconto()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
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

        public CheckoutInputDto CriarInputValidoSemFrete()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
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

        public CheckoutInputDto CriarInputValidoSomenteItens()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
                Cpf = "407.302.170-27",
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                }
            };
        }

        public CheckoutInputDto CriarInputValidoComCupom()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
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

        public CheckoutInputDto CriarInputValidoComFrete()
        {
            return new CheckoutInputDto
            {
                IdPedido = Guid.NewGuid(),
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
