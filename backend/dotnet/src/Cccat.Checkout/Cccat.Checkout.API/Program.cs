using Cccat.Checkout.API.Helpers;
using Cccat.Checkout.API.Settings;
using Cccat.Checkout.Application.Factories;
using Cccat.Checkout.Application.Gateways;
using Cccat.Checkout.Domain.Interfaces;
using Cccat.Checkout.Infra.Configurations;
using Cccat.Checkout.Infra.Factories;
using Cccat.Checkout.Infra.Gateways;
using Cccat.Checkout.Infra.HttpClients;
using Cccat.Checkout.Infra.Repositories;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseConfiguration(Environment.GetEnvironmentVariable("Conexao"));
builder.Services.AddScoped<UseCaseFactory>();
builder.Services.AddScoped<ICupomRepository, CupomRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<IGatewayFactory, GatewayHttpFactory>();
builder.Services.AddScoped<IFreteGateway, FreteHttpGateway>();
builder.Services.AddScoped<ICatalogoGateway, CatalogoHttpGateway>();

var urlSettings = new UrlSettings();
builder.Configuration.GetSection(nameof(UrlSettings)).Bind(urlSettings);

// Clients
builder.Services
    .AddRefitClient<IFreteHttpClient>()
    .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(urlSettings.APIFrete));

builder.Services
    .AddRefitClient<ICatalogoHttpClient>()
    .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(urlSettings.APICatalogo));

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
