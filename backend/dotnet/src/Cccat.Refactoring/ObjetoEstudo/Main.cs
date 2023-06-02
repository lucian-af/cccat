namespace Cccat.Refactoring.ObjetoEstudo
{
    public static class Main
    {
        // calcular corrida
        public static decimal CalcularCorrida(List<LsParams> lista)
        {
            var result = 0M;
            foreach (var item in lista)
            {
                if (int.TryParse(item.Dist, out var dist) && dist > 0)
                {
                    if (DateTime.TryParse(item.Ds, out var ds))
                    {
                        // durante a noite

                        if (ds.Hour >= 22 || ds.Hour <= 6)
                        {
                            // não for domingo
                            if (ds.DayOfWeek != DayOfWeek.Sunday)
                            {
                                result += dist * 3.90M;
                            }
                            else
                            {
                                result += dist * 5M;
                            }
                        }
                        else
                        {
                            // domingo

                            if (ds.DayOfWeek == DayOfWeek.Sunday)
                            {
                                result += dist * 2.9M;
                            }
                            else
                            {
                                result += dist * 2.1M;
                            }
                        }
                    }
                    else
                    {
                        return -2;
                    }
                }
                else
                {
                    return -1;
                }
            }
            if (result < 10)
            {
                return 10;
            }
            else
            {
                return result;
            }
        }
    }

    public class LsParams
    {
        public string Dist { get; set; }

        public string Ds { get; set; }
    }
}