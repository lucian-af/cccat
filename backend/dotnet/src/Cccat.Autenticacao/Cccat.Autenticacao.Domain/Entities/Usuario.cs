using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Cccat.Autenticacao.Domain.Entities;
public class Usuario
{
	public Guid Id { get; private set; }
	public Email Email { get; private set; }
	public string Senha { get; private set; }
	public string Salt { get; private set; }

	private Usuario() { }

	public Usuario(string email, string senha)
	{
		Id = Guid.NewGuid();
		Email = new Email(email);

		var hashSalt = "minha hash salt";
		var saltEmBytes = Encoding.UTF8.GetBytes(hashSalt);
		new Random().NextBytes(saltEmBytes);
		var senhaEmBytes = KeyDerivation.Pbkdf2(senha, saltEmBytes, KeyDerivationPrf.HMACSHA512, 64, 100);

		Senha = Convert.ToHexString(senhaEmBytes);
		Salt = Convert.ToHexString(saltEmBytes);
	}
}
