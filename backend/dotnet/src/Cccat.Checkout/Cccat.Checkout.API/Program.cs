using Cccat.Checkout.API.Configurations;
using Cccat.Checkout.API.Helpers;
using Cccat.Checkout.API.Settings;
using Cccat.Checkout.Application.Factories;
using Cccat.Checkout.Application.Gateways;
using Cccat.Checkout.Domain.Interfaces;
using Cccat.Checkout.Infra.Configurations;
using Cccat.Checkout.Infra.Factories;
using Cccat.Checkout.Infra.Gateways;
using Cccat.Checkout.Infra.Handlers;
using Cccat.Checkout.Infra.HttpClients;
using Cccat.Checkout.Infra.Repositories;
using Microsoft.OpenApi.Models;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseConfiguration(Environment.GetEnvironmentVariable("Conexao"));
builder.Services.AddScoped<UseCaseFactory>();
builder.Services.AddScoped<AutenticacaoHandler>();
builder.Services.AddScoped<ICupomRepository, CupomRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<IGatewayFactory, GatewayHttpFactory>();
builder.Services.AddScoped<IFreteGateway, FreteHttpGateway>();
builder.Services.AddScoped<ICatalogoGateway, CatalogoHttpGateway>();
builder.Services.AddScoped<AutenticacaoHttpGateway>();

var urlSettings = new UrlSettings();
builder.Configuration.GetSection(nameof(UrlSettings)).Bind(urlSettings);

// Clients
builder.Services
	.AddRefitClient<IFreteHttpClient>()
	.ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(urlSettings.APIFrete))
	.AddHttpMessageHandler<AutenticacaoHandler>();

builder.Services
	.AddRefitClient<ICatalogoHttpClient>()
	.ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(urlSettings.APICatalogo))
	.AddHttpMessageHandler<AutenticacaoHandler>();

builder.Services
	.AddRefitClient<IAutenticacaoHttpClient>()
	.ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(urlSettings.APIAutenticacao));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cccat.Checkout" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Adicione um token válido",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						System.Array.Empty<string>()
					}
				});
});
builder.Services.AddAutenticacao(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsStaging())
{
	app.ExecutarSeedDados().Wait();
}

app.UseAuthorization();
app.UseAuthentication();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

public partial class Program { }
