using EscolaIdiomas.Domain.Entities;
using EscolaIdiomas.Domain.Interfaces;
using Moq;

namespace EscolaIdiomas.Tests.Domain
{
    public class AlunoServiceTests
    {
        private readonly AlunoService _service;
        private readonly Mock<IAlunoRepository> _repositoryMock;

        public AlunoServiceTests()
        {
            _repositoryMock = new Mock<IAlunoRepository>();
            _service = new AlunoService(_repositoryMock.Object);
        }
        
        [Fact]
        public async Task CadastrarAluno_DeveCadastrarAluno_SeCpfNaoExistir()
        {
            _repositoryMock.Setup(r => r.ExistsByCpfAsync(It.IsAny<string>())).ReturnsAsync(false);

            await _service.CadastrarAlunoAsync("João", "12345678901");

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Aluno>()), Times.Once);
        }

    }
}
