using Cccat.Entities;
using Cccat.Entities.Interfaces;
using Cccat.Infra;
using Cccat.Infra.Repositories;
using Cccat.UseCases.Models;

namespace Cccat.Tests.Fixtures
{
    public class CalculaDistanciaFixture
    {
        private readonly DatabaseContext _dbContext;

        public CalculaDistanciaFixture(DatabaseContext dbContext)
            => _dbContext = dbContext;

        public CalculaDistanciaInputDto CriarInputValido()
        {
            return new CalculaDistanciaInputDto
            {
                CepOrigem = "17600090",
                CepDestino = "72980000"
            };
        }

        public ICepRepository CriarCepRepository(bool fake = true)
        {
            if (fake)
                return new CepRepositoryFake();

            return new CepRepository(_dbContext);
        }

        internal class CepRepositoryFake : ICepRepository
        {
            private static List<Cep> Ceps()
                => new()
                {
                    new Cep("17600090","Rua Cherentes","Centro",-21.940867,-50.506929),
                    new Cep("72980000","Rua Manoel","Vila Pratinha",-15.855776,-48.955921)
                };

            public void AdicionarCep(Cep cep)
                => throw new NotImplementedException();

            public Cep Get(string codigo)
                => Ceps().Find(cep => cep.Codigo.Equals(codigo));
        }
    }
}
