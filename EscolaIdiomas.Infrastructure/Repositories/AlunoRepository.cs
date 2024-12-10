using EscolaIdiomas.Domain.Entities;
using EscolaIdiomas.Domain.Interfaces;
using EscolaIdiomas.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

public class AlunoRepository : IAlunoRepository
{
    private readonly EscolaIdiomasContext _context;

    public AlunoRepository(EscolaIdiomasContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByCpfAsync(string cpf)
    {
        return await _context.Alunos.AnyAsync(a => a.Cpf == cpf);
    }

    public async Task<Aluno> GetByIdAsync(int id)
    {
        return await _context.Alunos.FindAsync(id);
    }

    public async Task<Aluno> GetByIdWithMatriculasAsync(int id)
    {
        return await _context.Alunos
            .Include(a => a.Matriculas)
            .ThenInclude(m => m.Turma)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Aluno aluno)
    {
        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Aluno aluno)
    {
        _context.Alunos.Remove(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Aluno>> GetAllAsync()
    {
        return await _context.Alunos
            .Include(a => a.Matriculas)
            .ThenInclude(m => m.Turma)
            .ToListAsync();
    }

    public async Task<Aluno> GetByCpfAsync(string cpf)
    {
        return await _context.Alunos
            .Include(a => a.Matriculas)
            .ThenInclude(m => m.Turma)
            .FirstOrDefaultAsync(a => a.Cpf == cpf);
    }



}
