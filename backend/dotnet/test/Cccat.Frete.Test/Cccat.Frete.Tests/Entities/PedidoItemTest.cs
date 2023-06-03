﻿using Cccat.Domain.Entities;

namespace Cccat.Tests.Entities
{
    public class PedidoItemTest
    {
        [Trait("Cccat", "Entities.PedidoItem")]
        [Fact]
        public void NaoDeveCriarItemComQuantidadeInvalida()
        {
            var result = Assert.Throws<Exception>(() => new PedidoItem(Guid.NewGuid(), 1, 1000, 0));

            Assert.NotNull(result);
            Assert.Equal("Quantidade inválida.", result.Message);
        }
    }
}