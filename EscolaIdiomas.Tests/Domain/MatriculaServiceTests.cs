using EscolaIdiomas.Application.Services;
using EscolaIdiomas.Domain.Exceptions;
using EscolaIdiomas.Domain.Interfaces;
using Moq;

namespace EscolaIdiomas.Tests.Domain
{
    public class MatriculaServiceTests
    {
        private readonly MatriculaService _service;
        private readonly Mock<IMatriculaRepository> _repositoryMock;
        private readonly Mock<AlunoRepository> _alunoRepository;
        private readonly Mock<ITurmaRepository> _turmaRepositoryMock;

        public MatriculaServiceTests()
        {
            _repositoryMock = new Mock<IMatriculaRepository>();
            _turmaRepositoryMock = new Mock<ITurmaRepository>();
            _service = new MatriculaService(_repositoryMock.Object, _turmaRepositoryMock.Object, _alunoRepository.Object);
        }

        [Fact]
        public async Task MatricularAluno_DeveRetornarErro_SeMatriculaJaExistir()
        {
            _repositoryMock.Setup(r => r.ExistsAsync(1, 1)).ReturnsAsync(true);

            await Assert.ThrowsAsync<DomainException>(
                () => _service.MatricularAlunoAsync(1, 1));
        }
    }
}

