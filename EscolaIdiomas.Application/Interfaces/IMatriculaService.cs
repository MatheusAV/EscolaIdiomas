using EscolaIdiomas.Domain.Dtos;

namespace EscolaIdiomas.Application.Interfaces
{
    public interface IMatriculaService
    {
        Task MatricularAlunoAsync(int alunoId, int turmaId);
        Task AtualizarMatriculaAsync(int matriculaId, int novoAlunoId, int novaTurmaId);
        Task ExcluirMatriculaAsync(int matriculaId);
        Task<IEnumerable<MatriculaDto>> GetMatriculasByAlunoCpfAsync(string cpf);
        Task DeleteMatriculaAsync(int matriculaId);
    }
}
