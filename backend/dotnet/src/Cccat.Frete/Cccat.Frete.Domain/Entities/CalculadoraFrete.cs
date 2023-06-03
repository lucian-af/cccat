namespace Cccat.Frete.Domain.Entities
{
    public static class CalculadoraFrete
    {
        private const int ValorFreteMinimo = 10;

        public static decimal Calcular(decimal distancia, decimal volume, decimal densidade)
        {
            var freteCalculado = volume * distancia * (densidade / 100);
            return Math.Max(ValorFreteMinimo, freteCalculado);
        }
    }
}
