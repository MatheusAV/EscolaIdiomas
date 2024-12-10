using EscolaIdiomas.Domain.Entities;

namespace EscolaIdiomas.Application.Interfaces
{
    using EscolaIdiomas.Domain.Dtos;

    public interface IAlunoService
    {
        Task CadastrarAlunoAsync(string nome, string cpf);
        Task<AlunoListDto> ObterAlunoComTurmasAsync(int id);
        Task AtualizarAlunoAsync(int id, AlunoDto alunoDto);
        Task DeletarAlunoAsync(int id);
        Task<IEnumerable<AlunoListDto>> ObterTodosAsync();        
        Task<AlunoListDto> ObterAlunoPorCpfAsync(string cpf);
    }


}
