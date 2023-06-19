namespace Cccat.Autenticacao.Infra.Repositories.Models;
public class UsuarioDb
{
	public Guid Id { get; set; }
	public string Email { get; set; }
	public string Senha { get; set; }
	public string Salt { get; set; }
	public int SenhaTipo { get; set; }
}
