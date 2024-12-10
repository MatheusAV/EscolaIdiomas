using API;
using EscolaIdiomas.Application.Interfaces;
using EscolaIdiomas.Application.Services;
using EscolaIdiomas.Domain.Interfaces;
using EscolaIdiomas.Infrastructure.Data.Context;
using EscolaIdiomas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Configura��o de pol�tica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader(); 
    });
});


DependencyInjection.Register(builder.Services);

builder.Services.AddDbContext<EscolaIdiomasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped // Configura��o de escopo adequada
);


// Configura��o de inje��o de depend�ncia
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configura��o do middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar CORS
app.UseCors("AllowAll");


app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
