using API.Helpers;
using Cccat.Estoque.API.Configurations;
using Cccat.Estoque.Application.Factories;
using Cccat.Estoque.BackgroundTask;
using Cccat.Estoque.BackgroundTask.Queue;
using Cccat.Estoque.BackgroundTask.Settings;
using Cccat.Estoque.Domain.Interfaces;
using Cccat.Estoque.Infra.Configurations;
using Cccat.Estoque.Infra.Factories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseConfiguration(Environment.GetEnvironmentVariable("Conexao"));
builder.Services.AddScoped<UseCaseFactory>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cccat.Estoque" });
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
builder.Services.AddScoped<IQueue, RabbitMqAdapter>();
builder.Services.AddHostedService<ProcessaEstoqueService>();
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));

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
