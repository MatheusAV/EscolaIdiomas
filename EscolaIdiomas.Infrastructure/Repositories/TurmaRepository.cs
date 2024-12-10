using EscolaIdiomas.Domain.Entities;
using EscolaIdiomas.Domain.Interfaces;
using EscolaIdiomas.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EscolaIdiomas.Infrastructure.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly EscolaIdiomasContext _context;
        

        public TurmaRepository(EscolaIdiomasContext context)
        {
            _context = context;            
        }

        public async Task<Turma> GetByIdAsync(int id)
        {
            return await _context.Turmas
                .Include(t => t.Matriculas)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Turma turma)
        {
            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Turma turma)
        {
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
        }

        public async Task<Turma> GetByIdWithMatriculasAsync(int id)
        {
            return await _context.Turmas
                .Include(t => t.Matriculas)
                    .ThenInclude(m => m.Aluno)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Turma>> GetAllAsync()
        {
            return await _context.Turmas.ToListAsync();
        }
    }
}