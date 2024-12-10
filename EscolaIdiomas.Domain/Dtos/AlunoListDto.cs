namespace EscolaIdiomas.Domain.Dtos
{
    public class AlunoListDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public List<string> Turmas { get; set; }
    }
}
