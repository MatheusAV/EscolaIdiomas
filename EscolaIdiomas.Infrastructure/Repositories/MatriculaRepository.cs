using EscolaIdiomas.Domain.Entities;
using EscolaIdiomas.Domain.Interfaces;
using EscolaIdiomas.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EscolaIdiomas.Infrastructure.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly EscolaIdiomasContext _context;        

        public MatriculaRepository(EscolaIdiomasContext context)
        {
            _context = context;           
        }

        public async Task<int> GetMatriculasCountByTurmaIdAsync(int turmaId)
        {
            return await _context.Matriculas.CountAsync(m => m.TurmaId == turmaId);
        }

        public async Task AddAsync(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int alunoId, int turmaId)
        {
            return await _context.Matriculas.AnyAsync(m => m.AlunoId == alunoId && m.TurmaId == turmaId);
        }
    }
}

