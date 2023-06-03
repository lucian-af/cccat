namespace Cccat.Catalogo.Domain.Entities
{
    public class Cupom
    {
        public int Id { get; private set; }
        public string Codigo { get; private set; }
        public decimal Percentual { get; private set; }
        public DateTime Validade { get; private set; }

        public Cupom(int id, string codigo, decimal percentual, DateTime validade)
        {
            Id = id;
            Codigo = codigo;
            Percentual = percentual;
            Validade = validade;
        }

        public bool Valido(DateTime dataExpiracao)
            => Validade >= dataExpiracao;

        public decimal CalcularDesconto(decimal valor)
            => valor * Percentual / 100;
    }
}
