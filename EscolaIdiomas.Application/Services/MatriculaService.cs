using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Domain.Entities;
using EscolaIdiomas.Domain.Exceptions;
using EscolaIdiomas.Domain.Interfaces;

namespace EscolaIdiomas.Application.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _repository;
        private readonly ITurmaRepository _turmaRepository;

        public MatriculaService(IMatriculaRepository repository, ITurmaRepository turmaRepository)
        {
            _repository = repository;
            _turmaRepository = turmaRepository;
        }

        public async Task MatricularAlunoAsync(int alunoId, int turmaId)
        {
            // Verificar se a matrícula já existe
            if (await _repository.ExistsAsync(alunoId, turmaId))
                throw new DomainException("Erro: Aluno já matriculado nesta turma.");

            // Verificar limite de alunos na turma
            var totalAlunos = await _repository.GetMatriculasCountByTurmaIdAsync(turmaId);
            if (totalAlunos >= 5)
                throw new DomainException("Erro: A turma já atingiu o limite de 5 alunos.");

            // Criar matrícula
            var matricula = Matricula.Criar(alunoId, turmaId);

            await _repository.AddAsync(matricula);
        }



        public async Task AtualizarMatriculaAsync(int matriculaId, int novoAlunoId, int novaTurmaId)
        {
            var matricula = await _repository.GetByIdAsync(matriculaId);
            if (matricula == null)
                throw new DomainException("Matrícula não encontrada.");

            // Atualiza a matrícula utilizando o método da entidade
            matricula.Atualizar(novoAlunoId, novaTurmaId);

            await _repository.UpdateAsync(matricula);
        }

        public async Task ExcluirMatriculaAsync(int matriculaId)
        {
            var matricula = await _repository.GetByIdAsync(matriculaId);
            if (matricula == null)
                throw new DomainException("Matrícula não encontrada.");

            await _repository.DeleteAsync(matriculaId);
        }
    }
}
