namespace Cccat.Autenticacao.Domain.Entities;
public abstract class Senha
{
	public string Valor { get; protected set; }
	public string Salt { get; protected set; }
	public virtual bool Validar(string senha) => senha.Equals(Valor);
}
