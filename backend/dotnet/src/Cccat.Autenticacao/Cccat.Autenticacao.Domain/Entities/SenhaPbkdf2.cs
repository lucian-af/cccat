using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Cccat.Autenticacao.Domain.Entities;
public class SenhaPbkdf2 : Senha
{
	private SenhaPbkdf2(string senha, string salt)
	{
		Valor = senha;
		Salt = salt;
	}
	public static Senha Criar(string senha)
	{
		var hashSalt = "minha hash salt";
		var saltEmBytes = Encoding.UTF8.GetBytes(hashSalt);
		new Random().NextBytes(saltEmBytes);
		var salt = Convert.ToHexString(saltEmBytes);

		var hash = KeyDerivation.Pbkdf2(senha, Convert.FromHexString(salt), KeyDerivationPrf.HMACSHA512, 64, 100);

		return new SenhaPbkdf2(Convert.ToHexString(hash), salt);
	}
	public static Senha Restaurar(string senha, string salt)
		=> new SenhaPbkdf2(senha, salt);
	public override bool Validar(string senha)
	{
		var hash = KeyDerivation.Pbkdf2(senha, Convert.FromHexString(Salt), KeyDerivationPrf.HMACSHA512, 64, 100);
		return Convert.ToHexString(hash).Equals(Valor);
	}
}
