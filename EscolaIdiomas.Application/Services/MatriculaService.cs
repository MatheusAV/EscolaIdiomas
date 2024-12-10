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
    }
}
