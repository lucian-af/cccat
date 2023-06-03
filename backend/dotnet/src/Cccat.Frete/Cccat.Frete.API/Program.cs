using Cccat.Frete.API.Helpers;
using Cccat.Frete.Application.Factories;
using Cccat.Frete.Domain.Interfaces;
using Cccat.Frete.Infra.Configurations;
using Cccat.Frete.Infra.Factories;
using Cccat.Frete.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseConfiguration(Environment.GetEnvironmentVariable("Conexao"));
builder.Services.AddScoped<UseCaseFactory>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICupomRepository, CupomRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ExecutarSeedDados().Wait();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
