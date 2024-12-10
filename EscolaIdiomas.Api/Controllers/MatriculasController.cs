using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Domain.Dtos;
using EscolaIdiomas.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

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
    }
}
