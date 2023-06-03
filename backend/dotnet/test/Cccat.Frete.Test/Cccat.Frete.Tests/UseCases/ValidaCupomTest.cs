﻿using Cccat.Frete.Application.UseCase;
using Cccat.Frete.Tests.Fixtures;

namespace Cccat.Frete.Tests.UseCases
{
    [Collection(nameof(DatabaseFixtureCollection))]
    public class ValidaCupomTest
    {
        private readonly ValidaCupom _validaCupom;

        public ValidaCupomTest(DatabaseFixture dbFixture)
        {
            var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
                .CriarRepositoryFactory();
            _validaCupom = new(factory);
        }

        [Trait("Cccat", "UseCases.Frete.ValidaCupom")]
        [Fact]
        public void DeveValidarSeCupomValido()
        {
            var codigo = "VALE20";

            var output = _validaCupom.EhValido(codigo);

            Assert.True(output);
        }

        [Trait("Cccat", "UseCases.Frete.ValidaCupom")]
        [Fact]
        public void DeveValidarSeCupomExpirado()
        {
            var codigo = "VALE50";

            var output = _validaCupom.EhValido(codigo);

            Assert.False(output);
        }
    }
}
