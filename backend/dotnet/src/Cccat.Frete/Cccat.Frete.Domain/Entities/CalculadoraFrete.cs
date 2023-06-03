namespace Cccat.Frete.Domain.Entities
{
    public static class CalculadoraFrete
    {
        private const int ValorFreteMinimo = 10;

        public static decimal Calcular(Produto produto)
        {
            var freteCalculado = produto.Volume() * 1000 * (produto.Densidade() / 100);
            return Math.Max(ValorFreteMinimo, freteCalculado);
        }
    }
}
