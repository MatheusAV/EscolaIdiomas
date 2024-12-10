using System;
using System.Linq;

namespace EscolaIdiomas.Domain.Validations
{
    public static class CpfValidator
    {
        public static void ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser vazio ou nulo.");

            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                throw new ArgumentException("CPF deve conter exatamente 11 dígitos.");

            if (TodosDigitosIguais(cpf))
                throw new ArgumentException("CPF não pode conter todos os dígitos iguais.");

            if (!VerificarDigitos(cpf))
                throw new ArgumentException("CPF inválido.");
        }

        private static bool TodosDigitosIguais(string cpf)
        {
            return cpf.All(c => c == cpf[0]);
        }

        private static bool VerificarDigitos(string cpf)
        {
            // Calcula o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (cpf[i] - '0') * (10 - i);

            int primeiroDigito = soma % 11 < 2 ? 0 : 11 - soma % 11;

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * (11 - i);

            int segundoDigito = soma % 11 < 2 ? 0 : 11 - soma % 11;

            // Verifica os dígitos calculados com os fornecidos
            return cpf[9] - '0' == primeiroDigito && cpf[10] - '0' == segundoDigito;
        }
    }
}
