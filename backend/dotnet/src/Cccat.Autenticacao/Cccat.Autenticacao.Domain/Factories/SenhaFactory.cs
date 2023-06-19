using Cccat.Autenticacao.Domain.Entities;
using Cccat.Autenticacao.Domain.Enums;

namespace Cccat.Autenticacao.Domain.Factories;
public static class SenhaFactory
{
	public static Senha Criar(SenhaTipo tipo, string senha)
		=> tipo switch
		{
			SenhaTipo.Pbkdf2 => SenhaPbkdf2.Criar(senha),
			SenhaTipo.Md5 => SenhaMd5.Criar(senha),
			_ => throw new NotImplementedException("Tipo de senha não encontrado.")
		};

	public static Senha Restaurar(SenhaTipo tipo, string senha, string salt)
		=> tipo switch
		{
			SenhaTipo.Pbkdf2 => SenhaPbkdf2.Restaurar(senha, salt),
			SenhaTipo.Md5 => SenhaMd5.Restaurar(senha),
			_ => throw new NotImplementedException("Tipo de senha não encontrado.")
		};
}
