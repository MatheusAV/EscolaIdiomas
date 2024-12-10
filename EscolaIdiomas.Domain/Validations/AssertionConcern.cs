using EscolaIdiomas.Domain.Exceptions;

namespace EscolaIdiomas.Domain.Validations
{
    public static class AssertionConcern
    {
        public static void AssertNotEmpty(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException(message);
        }

        public static void AssertCpf(string cpf, string message)
        {
            // Validação simples de CPF
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                throw new DomainException(message);
        }
    }
}
