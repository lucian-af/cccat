using Cccat.Frete.Domain.Entities;
using Cccat.Frete.Domain.Interfaces;
using Cccat.Frete.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Frete.Infra.Repositories
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
