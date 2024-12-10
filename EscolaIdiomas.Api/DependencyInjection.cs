using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Application.Services;
using EscolaIdiomas.Domain.Interfaces;
using EscolaIdiomas.Domain.Maper;
using EscolaIdiomas.Infrastructure.Repositories;

namespace API
{
    public class DependencyInjection
    {
        public static void Register(IServiceCollection services)
        {
            RepositoryDependence(services);
        }


        private static void RepositoryDependence(IServiceCollection services)
        {

            // Repositórios e Serviços
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IMatriculaRepository, MatriculaRepository>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<IMatriculaService, MatriculaService>();
            services.AddAutoMapper(typeof(MappingProfile));

        }
    }
}
