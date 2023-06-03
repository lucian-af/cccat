using System.Text.RegularExpressions;

namespace Cccat.Checkout.Domain.Entities
{
    public class Cpf
    {
        protected Cpf() { }

        public string Valor { get; private set; }

        public Cpf(string cpf)
        {
            if (!Validar(cpf))
                throw new Exception("Cpf inválido.");

            Valor = RemoverCaracteresEspeciais(cpf);
        }

        private static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            cpf = RemoverCaracteresEspeciais(cpf);

            if (!SomenteNumerosInteiros(cpf)) return false;

            if (!CpfComOnzeDigitos(cpf)) return false;

            if (CpfComDigitosIguais(cpf)) return false;

            int dg1 = CalcularDigitoCpf(cpf, 10);
            int dg2 = CalcularDigitoCpf(cpf, 11);

            var digitoVerificadorOrigem = cpf[^2..];
            var digitoVerificador = $"{dg1}{dg2}";

            return digitoVerificadorOrigem.Equals(digitoVerificador);
        }

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

        private static string RemoverCaracteresEspeciais(string texto)
            => new Regex("\\D").Replace(texto, "");
    }
}
