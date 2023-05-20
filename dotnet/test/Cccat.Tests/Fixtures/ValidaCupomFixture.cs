using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Cccat.Infra;
using Cccat.Infra.Repositories;

namespace Cccat.Tests.Fixtures
{
    public class ValidaCupomFixture
    {
        private readonly DatabaseContext _dbContext;

        public ValidaCupomFixture(DatabaseContext dbContext)
            => _dbContext = dbContext;

        public ICupomRepository CriarCupomRepository(bool fake = true)
        {
            if (fake)
                return new CupomRepositoryFake();

            return new CupomRepository(_dbContext);
        }

        internal class CupomRepositoryFake : ICupomRepository
        {
            private static List<Cupom> Cupons()
                => new()
                {
                    new Cupom(1, "VALE20", 20, DateTime.Today.AddDays(20)),
                    new Cupom(2, "VALE50", 50, DateTime.Today.AddDays(-10))
                };

            public Cupom Get(string codigo)
                => Cupons().Find(cupom => cupom.Codigo.Equals(codigo));
        }
    }
}
