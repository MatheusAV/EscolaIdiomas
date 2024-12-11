using EscolaIdiomas.Domain.Entities;

namespace EscolaIdiomas.Domain.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<int> GetMatriculasCountByTurmaIdAsync(int turmaId);
        Task AddAsync(Matricula matricula);
        Task<bool> ExistsAsync(int alunoId, int turmaId);
        Task<Matricula> GetByIdAsync(int id);
        Task UpdateAsync(Matricula matricula);
        Task DeleteAsync(int id);
        Task<IEnumerable<Matricula>> GetByAlunoIdAsync(int alunoId);
    }
}