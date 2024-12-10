using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EscolaIdiomas.Infrastructure.Data.Context
{
    public class EscolaIdiomasContextFactory : IDesignTimeDbContextFactory<EscolaIdiomasContext>
    {
        public EscolaIdiomasContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<EscolaIdiomasContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EscolaIdiomasContext(optionsBuilder.Options);
        }


    }
}
