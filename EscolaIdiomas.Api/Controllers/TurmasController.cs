using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Domain.Dtos;
using EscolaIdiomas.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Cadastra uma nova turma.")]
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
        [SwaggerOperation(Summary = "Obtém uma turma específica com os alunos vinculados.")]
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

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as turmas.")]
        public async Task<IActionResult> ListarTurmas()
        {
            var turmas = await _service.ObterTodasTurmasAsync();

            var turmaDtos = turmas.Select(t => new TurmaDto
            {
                Id = t.Id,
                Nome = t.Nome
            }).ToList();

            return Ok(turmaDtos);
        }
    }
}
