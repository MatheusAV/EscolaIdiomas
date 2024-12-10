using EscolaIdiomas.Domain.Entities;

namespace EscolaIdiomas.Application.Interfaces
{
    public interface ITurmaService
    {
        Task CadastrarTurmaAsync(string nome);
        Task ExcluirTurmaAsync(int turmaId);
        Task<Turma> ObterTurmaComAlunosAsync(int id);
        Task<List<Turma>> ObterTodasTurmasAsync();
    }
}
