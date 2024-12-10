using EscolaIdiomas.Application.Services;
using EscolaIdiomas.Domain.Entities;
using EscolaIdiomas.Domain.Exceptions;
using EscolaIdiomas.Domain.Interfaces;
using Moq;

namespace EscolaIdiomas.Tests.Domain
{
    public class TurmaServiceTests
    {
        private readonly TurmaService _service;
        private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
        private readonly Mock<IMatriculaRepository> _matriculaRepositoryMock;

        public TurmaServiceTests()
        {
            _turmaRepositoryMock = new Mock<ITurmaRepository>();
            _matriculaRepositoryMock = new Mock<IMatriculaRepository>();
            _service = new TurmaService(_turmaRepositoryMock.Object, _matriculaRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarTurma_DeveCadastrarComNomeValido()
        {
            // Arrange
            var nome = "Inglês Básico";

            // Act
            await _service.CadastrarTurmaAsync(nome);

            // Assert
            _turmaRepositoryMock.Verify(r => r.AddAsync(It.Is<Turma>(t => t.Nome == nome)), Times.Once);
        }

        [Fact]
        public async Task CadastrarTurma_DeveRetornarErro_SeNomeForInvalido()
        {
            // Arrange
            var nomeInvalido = "";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DomainException>(
                () => _service.CadastrarTurmaAsync(nomeInvalido)
            );

            Assert.Equal("Erro: O nome da turma é obrigatório.", exception.Message);
        }

        [Fact]
        public async Task ExcluirTurma_DeveExcluir_SeNaoTiverMatriculas()
        {
            // Arrange
            var turmaId = 1;
            var turma = new Turma("Inglês Básico");

            _turmaRepositoryMock.Setup(r => r.GetByIdAsync(turmaId)).ReturnsAsync(turma);
            _matriculaRepositoryMock.Setup(r => r.GetMatriculasCountByTurmaIdAsync(turmaId)).ReturnsAsync(0);

            // Act
            await _service.ExcluirTurmaAsync(turmaId);

            // Assert
            _turmaRepositoryMock.Verify(r => r.DeleteAsync(turma), Times.Once);
        }

        [Fact]
        public async Task ExcluirTurma_DeveRetornarErro_SeTiverMatriculas()
        {
            // Arrange
            var turmaId = 1;
            var turma = new Turma("Inglês Básico");

            _turmaRepositoryMock.Setup(r => r.GetByIdAsync(turmaId)).ReturnsAsync(turma);
            _matriculaRepositoryMock.Setup(r => r.GetMatriculasCountByTurmaIdAsync(turmaId)).ReturnsAsync(1);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DomainException>(
                () => _service.ExcluirTurmaAsync(turmaId)
            );

            Assert.Equal("Erro: Não é possível excluir uma turma com alunos matriculados.", exception.Message);
            _turmaRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Turma>()), Times.Never);
        }

        [Fact]
        public async Task ExcluirTurma_DeveRetornarErro_SeTurmaNaoExistir()
        {
            // Arrange
            var turmaId = 999;

            _turmaRepositoryMock.Setup(r => r.GetByIdAsync(turmaId)).ReturnsAsync((Turma)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DomainException>(
                () => _service.ExcluirTurmaAsync(turmaId)
            );

            Assert.Equal("Erro: Turma não encontrada.", exception.Message);
            _turmaRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Turma>()), Times.Never);
        }
    }
}
