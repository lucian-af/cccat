using System.Text.RegularExpressions;

namespace Cccat.Application.Helpers
{
    // TODO: mover para outra camada
    public static partial class ValidarCpf
    {
        private static bool SomenteNumerosInteiros(string cpf)
            => long.TryParse(cpf, out var _);

        private static bool CpfComOnzeDigitos(string cpf)
            => cpf.Length == 11;

        private static bool CpfComDigitosIguais(string cpf)
            => cpf.All(c => c == cpf[0]);

        private static int CalcularDigitoCpf(string cpf, int fator)
        {
            var total = 0;

            foreach (var digito in cpf)
            {
                if (fator < 2) break;

                total += int.Parse(digito.ToString()) * fator--;
            }

            var restoDivisao = total % 11;

            return restoDivisao < 2 ? 0 : 11 - restoDivisao;
        }

        public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            cpf = NaoDigitos().Replace(cpf, "");

            if (!SomenteNumerosInteiros(cpf)) return false;

            if (!CpfComOnzeDigitos(cpf)) return false;

            if (CpfComDigitosIguais(cpf)) return false;

            int dg1 = CalcularDigitoCpf(cpf, 10);
            int dg2 = CalcularDigitoCpf(cpf, 11);

            var digitoVerificadorOrigem = cpf[^2..];
            var digitoVerificador = $"{dg1}{dg2}";
            return digitoVerificadorOrigem.Equals(digitoVerificador);
        }

        [GeneratedRegex(@"\D")]
        private static partial Regex NaoDigitos();
    }
}
