using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Cccat.Autenticacao.Domain.Services;
public static class TokenJwt
{
	private const string Emissor = "Cccat";
	private const string PublicoAlvo = "Cccat";
	private const string Chave = "<um-super-segredo>";

	public static string Gerar(DateTime dataExpiracao, Dictionary<string, string> payload = null)
	{
		var hashKey = ConverterChaveEmBytes(Chave);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Issuer = Emissor,
			Audience = PublicoAlvo,
			Expires = dataExpiracao.ToUniversalTime(),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(hashKey), SecurityAlgorithms.HmacSha256),
			Subject = new ClaimsIdentity(AdicionarClaims(payload))
		};
		var tokenHandler = new JwtSecurityTokenHandler();
		return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
	}

	public static bool Validar(string token)
	{
		try
		{
			var hashKey = ConverterChaveEmBytes(Chave);

			new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				IssuerSigningKey = new SymmetricSecurityKey(hashKey),
				ValidIssuer = Emissor,
				ValidAudience = PublicoAlvo,
				ClockSkew = TimeSpan.Zero,
			}, out var _);

			return true;
		}
		catch { return false; }
	}

	private static List<Claim> AdicionarClaims(Dictionary<string, string> payload)
	{
		var claims = new List<Claim>();

		if (payload?.Count > 0)
			foreach (var (key, value) in payload)
				claims.Add(new(key, value));

		claims.Add(new(JwtRegisteredClaimNames.Iat, DateTime.Today.ToString()));

		return claims;
	}

	private static byte[] ConverterChaveEmBytes(string chave)
		=> Encoding.ASCII.GetBytes(chave);
}
