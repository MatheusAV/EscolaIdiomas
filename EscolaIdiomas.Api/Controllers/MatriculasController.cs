using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Application.Services;
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

        //[HttpDelete("{id}")]
        //[SwaggerOperation(Summary = "Exclui uma matr�cula existente.")]
        //public async Task<IActionResult> ExcluirMatricula(int id)
        //{
        //    try
        //    {
        //        await _service.ExcluirMatriculaAsync(id);
        //        return Ok(new { Message = "Matr�cula exclu�da com sucesso!" });
        //    }
        //    catch (DomainException ex)
        //    {
        //        return BadRequest(new { Error = ex.Message });
        //    }
        //}
        
        [HttpGet("Aluno/{cpf}")]
        [SwaggerOperation(Summary = "Lista todas as matr�culas de um aluno com suas turmas pelo CPF")]
        public async Task<IActionResult> GetMatriculasByAlunoCpf(string cpf)
        {
            try
            {
                var matriculas = await _service.GetMatriculasByAlunoCpfAsync(cpf);
                if (matriculas == null || !matriculas.Any())
                {
                    return Ok(new { Message = "Nenhuma matr�cula encontrada para o CPF informado." });
                }

                return Ok(matriculas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui uma matr�cula existente associada ao aluno.")]
        public async Task<IActionResult> DeleteMatricula(int id)
        {
            try
            {
                await _service.DeleteMatriculaAsync(id);
                return Ok(new { Message = "Matr�cula exclu�da com sucesso. O aluno foi desassociado da turma." });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

    }
}
