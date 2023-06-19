using Cccat.Autenticacao.Domain.Enums;
using Cccat.Autenticacao.Domain.Factories;

namespace Cccat.Autenticacao.Domain.Entities;
public class Usuario
{
	public Guid Id { get; private set; }
	public Email Email { get; private set; }
	public Senha Senha { get; private set; }
	public SenhaTipo SenhaTipo { get; private set; }

	private Usuario(string email, Senha senha, SenhaTipo senhaTipo = SenhaTipo.Pbkdf2)
	{
		Id = Guid.NewGuid();
		Email = new Email(email);
		Senha = senha;
		SenhaTipo = senhaTipo;
	}

	private Usuario(Guid id, string email, Senha senha, SenhaTipo senhaTipo = SenhaTipo.Pbkdf2)
	{
		Id = id;
		Email = new Email(email);
		Senha = senha;
		SenhaTipo = senhaTipo;
	}

	public static Usuario Criar(string email, string senha, SenhaTipo senhaTipo = SenhaTipo.Pbkdf2)
		=> new(email, SenhaFactory.Criar(senhaTipo, senha), senhaTipo);

	public static Usuario Restaurar(Guid id, string email, string senha, string salt, SenhaTipo senhaTipo)
		=> new(id, email, SenhaFactory.Restaurar(senhaTipo, senha, salt), senhaTipo);

	public bool ValidarSenha(string senha)
		=> Senha.Validar(senha);
}
