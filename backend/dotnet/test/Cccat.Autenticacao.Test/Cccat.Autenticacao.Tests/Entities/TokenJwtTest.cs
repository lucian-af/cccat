using Cccat.Autenticacao.Domain.Services;

namespace Cccat.Autenticacao.Tests.Entities;
public class TokenJwtTest
{
	[Trait("Cccat", "Domain.Services.Autenticacao.TokenJwt")]
	[Fact]
	public void DeveGerarTokenComPayload()
	{
		var payload = new Dictionary<string, string>
		{
			{ "email", "teste@teste.com.br" },
			{ "name", "lucian" },
		};
		var tokenGerado = TokenJwt.Gerar(DateTime.Now.AddHours(1), payload);
		Assert.False(string.IsNullOrWhiteSpace(tokenGerado));
	}

	[Trait("Cccat", "Domain.Services.Autenticacao.TokenJwt")]
	[Fact]
	public void DeveGerarTokenSemPayload()
	{
		var tokenGerado = TokenJwt.Gerar(DateTime.Now.AddHours(1));
		Assert.False(string.IsNullOrWhiteSpace(tokenGerado));
	}

	[Trait("Cccat", "Domain.Services.Autenticacao.TokenJwt")]
	[Fact]
	public void DeveRetornarVerdadeiroSeTokenValidoComPayload()
	{
		var payload = new Dictionary<string, string>
		{
			{ "email", "teste@teste.com.br" },
			{ "name", "lucian" },
		};
		var token = TokenJwt.Gerar(DateTime.Now.AddHours(1), payload);
		Assert.True(TokenJwt.Validar(token));
	}

	[Trait("Cccat", "Domain.Services.Autenticacao.TokenJwt")]
	[Fact]
	public void DeveRetornarVerdadeiroSeTokenValidoSemPayload()
	{
		var token = TokenJwt.Gerar(DateTime.Now.AddHours(1));
		Assert.True(TokenJwt.Validar(token));
	}

	[Trait("Cccat", "Domain.Services.Autenticacao.TokenJwt")]
	[Fact]
	public async Task DeveRetornarFalsoSeTokenExpirado()
	{
		var token = TokenJwt.Gerar(DateTime.Now.AddSeconds(10));
		await Task.Delay(TimeSpan.FromSeconds(11));
		Assert.False(TokenJwt.Validar(token));
	}
}
