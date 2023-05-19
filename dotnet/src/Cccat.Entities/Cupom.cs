namespace Cccat.Entities
{
    public class Cupom
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public decimal Percentual { get; set; }
        public DateTime Validade { get; set; }

        public bool Valido(DateTime dataExpiracao)
            => Validade >= dataExpiracao;
    }
}
