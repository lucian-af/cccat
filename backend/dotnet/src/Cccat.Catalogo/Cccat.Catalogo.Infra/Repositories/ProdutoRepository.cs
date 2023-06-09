﻿using Cccat.Catalogo.Domain.Entities;
using Cccat.Catalogo.Domain.Interfaces;
using Cccat.Catalogo.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Catalogo.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DatabaseContext _context;

        public ProdutoRepository(DatabaseContext context)
            => _context = context;

        public Produto Get(int idProduto)
            => _context.Produtos.Find(idProduto);

        public IEnumerable<Produto> All()
            => _context.Produtos.AsNoTracking();
    }
}
