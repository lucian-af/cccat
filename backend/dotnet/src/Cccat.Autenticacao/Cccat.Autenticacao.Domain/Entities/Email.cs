using System.Text.RegularExpressions;

namespace Cccat.Autenticacao.Domain.Entities;
public class Email
{
	public string Valor { get; private set; }

	private Email() { }

	public Email(string email)
	{
		if (!Validar(email))
			throw new Exception("E-mail inválido.");

		Valor = email;
	}

	private static bool Validar(string email)
	{
		var pattern = "(([^<>()[\\]\\\\.,;:\\s@\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))";
		return new Regex(pattern).IsMatch(email.ToLower());
	}
}
