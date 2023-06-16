using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Cccat.Autenticacao.Domain.Entities;
public class Senha
{
	public string Valor { get; private set; }
	public string Salt { get; private set; }

	private Senha() { }

	public Senha(string senha)
	{
		var hashSalt = "minha hash salt";
		var saltEmBytes = Encoding.UTF8.GetBytes(hashSalt);
		new Random().NextBytes(saltEmBytes);
		Salt = Convert.ToHexString(saltEmBytes);

		var hash = KeyDerivation.Pbkdf2(senha, Convert.FromHexString(Salt), KeyDerivationPrf.HMACSHA512, 64, 100);
		Valor = Convert.ToHexString(hash);
	}

	public bool Validar(string senha)
	{
		var hash = KeyDerivation.Pbkdf2(senha, Convert.FromHexString(Salt), KeyDerivationPrf.HMACSHA512, 64, 100);
		return Convert.ToHexString(hash).Equals(Valor);
	}
}
