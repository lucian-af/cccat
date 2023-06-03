﻿using Cccat.Frete.Domain.Interfaces;
using Cccat.Frete.Infra.Database;
using Cccat.Frete.Infra.Repositories;

namespace Cccat.Frete.Infra.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DatabaseContext _context;

        public RepositoryFactory(DatabaseContext context)
            => _context = context;

        public ICepRepository CriarCepRepository()
            => new CepRepository(_context);

        public ICupomRepository CriarCupomRepository()
            => new CupomRepository(_context);

        public IPedidoRepository CriarPedidoRepository()
            => new PedidoRepository(_context);

        public IProdutoRepository CriarProdutoRepository()
            => new ProdutoRepository(_context);
    }
}
