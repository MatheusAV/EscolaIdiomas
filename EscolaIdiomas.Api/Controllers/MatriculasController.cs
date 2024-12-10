using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Domain.Dtos;
using EscolaIdiomas.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace EscolaIdiomas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculaService _service;

        public MatriculasController(IMatriculaService service)
        {
            _service = service;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Matricula um aluno em uma turma.")]
        public async Task<IActionResult> MatricularAluno([FromBody] MatriculaDto matriculaDto)
        {
            try
            {
                await _service.MatricularAlunoAsync(matriculaDto.AlunoId, matriculaDto.TurmaId);
                return Ok(new { Message = "Aluno matriculado com sucesso!" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma matr�cula existente.")]
        public async Task<IActionResult> AtualizarMatricula(int id, [FromBody] MatriculaDto matriculaDto)
        {
            try
            {
                await _service.AtualizarMatriculaAsync(id, matriculaDto.AlunoId, matriculaDto.TurmaId);
                return Ok(new { Message = "Matr�cula atualizada com sucesso!" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui uma matr�cula existente.")]
        public async Task<IActionResult> ExcluirMatricula(int id)
        {
            try
            {
                await _service.ExcluirMatriculaAsync(id);
                return Ok(new { Message = "Matr�cula exclu�da com sucesso!" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
