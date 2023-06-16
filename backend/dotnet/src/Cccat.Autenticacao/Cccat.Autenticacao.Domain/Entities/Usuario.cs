namespace Cccat.Autenticacao.Domain.Entities;
public class Usuario
{
	public Guid Id { get; private set; }
	public Email Email { get; private set; }
	public Senha Senha { get; private set; }

	private Usuario() { }

	public Usuario(string email, string senha)
	{
		Id = Guid.NewGuid();
		Email = new Email(email);
		Senha = new(senha);
	}

	public bool ValidarSenha(string senha)
		=> Senha.Validar(senha);
}
