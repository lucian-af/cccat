using System.Security.Cryptography;
using System.Text;

namespace Cccat.Autenticacao.Domain.Entities;

// NOTE: tipo de "segurança" criado somente para fins de estudo, não recomendado para uso em produção.
public class SenhaMd5 : Senha
{
	private SenhaMd5(string senha, bool criar = true)
		=> Valor = criar ? Convert.ToHexString(GerarHash(senha)) : senha;

	public static SenhaMd5 Criar(string senha)
		=> new(senha);

	public static SenhaMd5 Restaurar(string senha)
		=> new(senha, false);

	public override bool Validar(string senha)
		=> Convert.ToHexString(GerarHash(senha)).Equals(Valor);

	private static byte[] GerarHash(string senha) => MD5.HashData(Encoding.UTF8.GetBytes(senha));
}
