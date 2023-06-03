﻿using Cccat.Checkout.Application.Models;
using Cccat.Checkout.Application.UseCase;
using Cccat.Checkout.Tests.Fixtures;

namespace Cccat.Checkout.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class SimulaFreteTest
    {
        private readonly SimulaFrete _simulaFrete;

        public SimulaFreteTest(DatabaseFixture dbFixture)
        {
            var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
                .CriarRepositoryFactory();
            _simulaFrete = new SimulaFrete(factory);
        }

        [Trait("Cccat", "UseCases.Checkout.SimulaFrete")]
        [Fact]
        public void DeveSimularFrete()
        {
            var input = new SimulaFreteInputDto
            {
                Items = new()
                {
                    new() { IdProduto = 1, Quantidade = 1 },
                    new () { IdProduto = 2, Quantidade = 1 },
                    new () { IdProduto = 3, Quantidade = 3 }
                },
                CepOrigem = "17600090",
                CepDestino = "17602700"
            };

            var output = _simulaFrete.Simular(input);

            Assert.Equal(280, output.Frete);
        }
    }
}
