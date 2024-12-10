using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Domain.Dtos;
using EscolaIdiomas.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EscolaIdiomas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly ITurmaService _service;

        public TurmasController(ITurmaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarTurma([FromBody] TurmaDto turmaDto)
        {
            try
            {
                await _service.CadastrarTurmaAsync(turmaDto.Nome);
                return Ok(new { Message = "Turma cadastrada com sucesso!" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterTurma(int id)
        {
            var turma = await _service.ObterTurmaComAlunosAsync(id);
            if (turma == null)
            {
                return NotFound(new { Error = "Turma não encontrada." });
            }

            var turmaDto = new TurmaListDto
            {
                Id = turma.Id,
                Nome = turma.Nome,
                Alunos = turma.Matriculas.Select(m => m.Aluno.Nome).ToList()
            };

            return Ok(turmaDto);
        }
    }
}
