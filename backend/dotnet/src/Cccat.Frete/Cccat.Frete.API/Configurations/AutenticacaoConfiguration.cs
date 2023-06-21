using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Cccat.Frete.API.Configurations;

public static class AutenticacaoConfiguration
{
	public static IServiceCollection AddAutenticacao(this IServiceCollection services, IConfiguration configuration)
	{
		var token = configuration.GetSection("Token").Value;
		if (string.IsNullOrEmpty(token)) return services;

		var hashKey = Encoding.ASCII.GetBytes(token);
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
				{
					x.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						ValidateIssuer = false,
						ValidateAudience = false,
						IssuerSigningKey = new SymmetricSecurityKey(hashKey),
						ClockSkew = TimeSpan.Zero,
					};
				});

		return services;
	}
}
