namespace EscolaIdiomas.Application.Interfaces
{
    public interface IMatriculaService
    {
        Task MatricularAlunoAsync(int alunoId, int turmaId);
    }
}
