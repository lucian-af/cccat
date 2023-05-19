using Cccat.Entities;
using Cccat.Entities.Interfaces;

namespace Cccat.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DatabaseContext _context;

        public ProdutoRepository(DatabaseContext context)
            => _context = context;

        public Produto Get(int idProduto)
            => _context.Produtos.Find(idProduto);
    }
}
