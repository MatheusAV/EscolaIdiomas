using AutoMapper;
using EscolaIdiomas.Domain.Dtos;
using EscolaIdiomas.Domain.Entities;

namespace EscolaIdiomas.Domain.Maper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de Aluno para AlunoDto
            CreateMap<Aluno, AlunoDto>()
                .ForMember(dest => dest.Turmas, opt => opt.MapFrom(src => src.Matriculas.Select(m => m.Turma).ToList()));                     

            // Mapeamento de Turma para TurmaDto
            CreateMap<Turma, TurmaDto>();
        }
    }
}
