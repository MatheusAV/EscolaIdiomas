using EscolaIdiomas.Domain.Validations;

namespace EscolaIdiomas.Domain.Entities
{
    public class Aluno
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public ICollection<Matricula> Matriculas { get; private set; }
        //public bool Ativo { get; set; } = true;

        public Aluno(string nome, string cpf)
        {
            AssertionConcern.AssertNotEmpty(nome, "O nome é obrigatório.");
            AssertionConcern.AssertCpf(cpf, "CPF inválido.");

            Nome = nome;
            Cpf = cpf;
            Matriculas = new List<Matricula>();
        }

        public void AtualizarNome(string nome)
        {
            AssertionConcern.AssertNotEmpty(nome, "O nome é obrigatório.");
            Nome = nome;
        }

        public void AtualizarCpf(string cpf)
        {
            AssertionConcern.AssertCpf(cpf, "CPF inválido.");
            Cpf = cpf;
        }
    }
}
