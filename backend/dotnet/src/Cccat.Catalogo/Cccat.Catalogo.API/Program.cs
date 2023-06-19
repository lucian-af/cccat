using Cccat.Catalogo.API.Helpers;
using Cccat.Catalogo.Application.Factories;
using Cccat.Catalogo.Domain.Interfaces;
using Cccat.Catalogo.Infra.Configurations;
using Cccat.Catalogo.Infra.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseConfiguration(Environment.GetEnvironmentVariable("Conexao"));
builder.Services.AddScoped<UseCaseFactory>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsStaging())
{
	app.ExecutarSeedDados().Wait();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();
app.Run();

public partial class Program { }
