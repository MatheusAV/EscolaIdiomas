using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Domain.Dtos;
using EscolaIdiomas.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EscolaIdiomas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _service;

        public AlunosController(IAlunoService service)
        {
            _service = service;
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um aluno existente")]
        public async Task<IActionResult> AtualizarAluno(int id, [FromBody] AlunoDto alunoDto)
        {
            try
            {
                await _service.AtualizarAlunoAsync(id, alunoDto);
                return Ok(new { Message = "Aluno atualizado com sucesso!" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui um aluno pelo ID")]
        public async Task<IActionResult> DeletarAluno(int id)
        {
            try
            {
                await _service.DeletarAlunoAsync(id);
                return Ok(new { Message = "Aluno excluído com sucesso!" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém a lista de todos os alunos")]
        public async Task<IActionResult> ObterTodosAlunos()
        {
            var alunos = await _service.ObterTodosAsync();
            return Ok(alunos);
        }

        [HttpGet("cpf/{cpf}")]
        [SwaggerOperation(Summary = "Busca um aluno pelo CPF")]
        public async Task<IActionResult> ObterAlunoPorCpf(string cpf)
        {
            try
            {
                var aluno = await _service.ObterAlunoPorCpfAsync(cpf);
                return Ok(aluno);
            }
            catch (DomainException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }


    }
}
