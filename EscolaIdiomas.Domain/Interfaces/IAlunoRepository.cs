using EscolaIdiomas.Domain.Entities;

namespace EscolaIdiomas.Domain.Interfaces
{
    public interface IAlunoRepository
    {
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<Aluno> GetByIdAsync(int id);
        Task<Aluno> GetByIdWithMatriculasAsync(int id);
        Task AddAsync(Aluno aluno);
        Task UpdateAsync(Aluno aluno);
        Task DeleteAsync(Aluno aluno);
        Task<IEnumerable<Aluno>> GetAllAsync();
        Task<Aluno> GetByCpfAsync(string cpf);
    }
}
