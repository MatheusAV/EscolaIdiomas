namespace EscolaIdiomas.Domain.Dtos
{
    public class TurmaListDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<string> Alunos { get; set; }
    }
}
