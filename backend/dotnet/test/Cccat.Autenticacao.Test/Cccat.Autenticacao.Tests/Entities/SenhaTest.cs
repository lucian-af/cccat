using Cccat.Autenticacao.Domain.Entities;

namespace Cccat.Autenticacao.Tests.Entities;
public class SenhaTest
{
	[Trait("Cccat", "Entities.Autenticacao.Senha")]
	[Fact]
	public void DeveCriarSenhaPbkdf2Valida()
	{
		var senha = SenhaPbkdf2.Criar("Ab21234!");

		var senhaValida = senha.Validar("Ab21234!");

		Assert.True(senhaValida);
	}

	[Trait("Cccat", "Entities.Autenticacao.Senha")]
	[Fact]
	public void DeveRetornarFalseSeSenhaPbkdf2Invalida()
	{
		var senha = SenhaPbkdf2.Criar("Ab21234!");

		var senhaValida = senha.Validar("outrasenha");

		Assert.False(senhaValida);
	}

	[Trait("Cccat", "Entities.Autenticacao.Senha")]
	[Fact]
	public void DeveCriarSenhaMd5Valida()
	{
		var senha = SenhaMd5.Criar("Q1w2E3");

		var senhaValida = senha.Validar("Q1w2E3");

		Assert.True(senhaValida);
	}

	[Trait("Cccat", "Entities.Autenticacao.Senha")]
	[Fact]
	public void DeveRetornarFalseSeSenhaMd5Invalida()
	{
		var senha = SenhaMd5.Criar("Q1w2E3");

		var senhaValida = senha.Validar("outrasenha");

		Assert.False(senhaValida);
	}
}
