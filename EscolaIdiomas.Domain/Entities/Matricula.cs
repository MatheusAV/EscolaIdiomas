namespace EscolaIdiomas.Domain.Entities
{
    public class Matricula
    {
        public int Id { get; private set; }
        public int AlunoId { get; private set; }
        public int TurmaId { get; private set; }
        public Aluno Aluno { get; private set; }
        public Turma Turma { get; private set; }

        
        public static Matricula Criar(int alunoId, int turmaId)
        {
            return new Matricula
            {
                AlunoId = alunoId,
                TurmaId = turmaId
            };
        }
        
        public void Atualizar(int alunoId, int turmaId)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
        }
    }
}