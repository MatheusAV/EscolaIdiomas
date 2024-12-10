using EscolaIdiomas.Domain.Entities;

namespace EscolaIdiomas.Domain.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<int> GetMatriculasCountByTurmaIdAsync(int turmaId);
        Task AddAsync(Matricula matricula);
        Task<bool> ExistsAsync(int alunoId, int turmaId);        
    }
}
