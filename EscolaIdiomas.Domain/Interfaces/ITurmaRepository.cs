using EscolaIdiomas.Domain.Entities;

namespace EscolaIdiomas.Domain.Interfaces
{
    public interface ITurmaRepository
    {
        Task<Turma> GetByIdAsync(int id);
        Task AddAsync(Turma turma);
        Task DeleteAsync(Turma turma);
        Task<Turma> GetByIdWithMatriculasAsync(int id);
        Task<List<Turma>> GetAllAsync();
    }
}
