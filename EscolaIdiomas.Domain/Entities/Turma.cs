using EscolaIdiomas.Domain.Validations;

namespace EscolaIdiomas.Domain.Entities
{
    public class Turma
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public ICollection<Matricula> Matriculas { get; private set; }

        public Turma(string nome)
        {
            AssertionConcern.AssertNotEmpty(nome, "O nome da turma é obrigatório.");
            Nome = nome;
            Matriculas = new List<Matricula>();
        }
    }
}
