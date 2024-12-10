using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Domain.Entities;
using EscolaIdiomas.Domain.Exceptions;
using EscolaIdiomas.Domain.Interfaces;

namespace EscolaIdiomas.Application.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _repository;
        private readonly IMatriculaRepository _matriculaRepository;

        public TurmaService(ITurmaRepository repository, IMatriculaRepository matriculaRepository)
        {
            _repository = repository;
            _matriculaRepository = matriculaRepository;
        }

        public async Task CadastrarTurmaAsync(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Erro: O nome da turma é obrigatório.");

            var turma = new Turma(nome);
            await _repository.AddAsync(turma);
        }

        public async Task ExcluirTurmaAsync(int turmaId)
        {
            var turma = await _repository.GetByIdAsync(turmaId);
            if (turma == null)
                throw new DomainException("Erro: Turma não encontrada.");

            var possuiAlunos = await _matriculaRepository.GetMatriculasCountByTurmaIdAsync(turmaId) > 0;
            if (possuiAlunos)
                throw new DomainException("Erro: Não é possível excluir uma turma com alunos matriculados.");

            await _repository.DeleteAsync(turma);
        }

        public async Task<Turma> ObterTurmaComAlunosAsync(int id)
        {
            return await _repository.GetByIdWithMatriculasAsync(id);
        }
    }
}
