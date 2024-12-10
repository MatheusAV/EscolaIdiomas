using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Domain.Dtos;
using EscolaIdiomas.Domain.Entities;
using EscolaIdiomas.Domain.Exceptions;
using EscolaIdiomas.Domain.Interfaces;
using AutoMapper;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _repository;
    private readonly IMapper _mapper;
    public AlunoService(IAlunoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CadastrarAlunoAsync(string nome, string cpf)
    {
        if (await _repository.ExistsByCpfAsync(cpf))
            throw new DomainException("Erro: Já existe um aluno com este CPF.");

        var aluno = new Aluno(nome, cpf);
        await _repository.AddAsync(aluno);
    }

    public async Task<AlunoListDto> ObterAlunoComTurmasAsync(int id)
    {
        var aluno = await _repository.GetByIdWithMatriculasAsync(id);
        if (aluno == null)
            throw new DomainException("Erro: Aluno não encontrado.");

        return new AlunoListDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Cpf = aluno.Cpf,
            Turmas = aluno.Matriculas.Select(m => m.Turma.Nome).ToList()
        };
    }



    public async Task AtualizarAlunoAsync(int id, AlunoDto alunoDto)
    {
        var aluno = await _repository.GetByIdAsync(id);
        if (aluno == null)
            throw new DomainException("Erro: Aluno não encontrado.");

        aluno.AtualizarNome(alunoDto.Nome);
        aluno.AtualizarCpf(alunoDto.Cpf);

        await _repository.UpdateAsync(aluno);
    }

    public async Task<AlunoDto> ObterAlunoPorIdAsync(int id)
    {
        var aluno = await _repository.GetByIdAsync(id);

        if (aluno == null)
            throw new DomainException("Aluno não encontrado.");

        return _mapper.Map<AlunoDto>(aluno);
    }



    public async Task DeletarAlunoAsync(int id)
    {
        var aluno = await _repository.GetByIdWithMatriculasAsync(id);
        if (aluno == null)
            throw new DomainException("Erro: Aluno não encontrado.");

        if (aluno.Matriculas.Any())
            throw new DomainException("Erro: Não é possível excluir um aluno matriculado.");

        await _repository.DeleteAsync(aluno);
    }

    public async Task<IEnumerable<AlunoListDto>> ObterTodosAsync()
    {
        var alunos = await _repository.GetAllAsync();
        return alunos.Select(a => new AlunoListDto
        {
            Id = a.Id,
            Nome = a.Nome,
            Cpf = a.Cpf,
            Turmas = a.Matriculas.Select(m => m.Turma.Nome).ToList()
        });
    }

    public async Task<AlunoListDto> ObterAlunoPorCpfAsync(string cpf)
    {
        var aluno = await _repository.GetByCpfAsync(cpf);
        if (aluno == null)
            throw new DomainException("Erro: Aluno não encontrado com este CPF.");

        return new AlunoListDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Cpf = aluno.Cpf,
            Turmas = aluno.Matriculas.Select(m => m.Turma.Nome).ToList()
        };
    }





}
