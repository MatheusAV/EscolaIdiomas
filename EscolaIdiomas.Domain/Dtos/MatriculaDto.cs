namespace EscolaIdiomas.Domain.Dtos
{
    public class MatriculaDto
    {
        public int MatriculaId { get; set; } 
        public int AlunoId { get; set; }    
        public string AlunoNome { get; set; } 
        public int TurmaId { get; set; }     
        public string TurmaNome { get; set; } 
    }

}
