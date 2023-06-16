using Cccat.Autenticacao.Domain.Entities;

namespace Cccat.Autenticacao.Domain.Interfaces;
public interface IUsuarioRepository
{
	public Task Cadastrar(Usuario usuario);

	public Task<Usuario> ObterUsuarioPorEmail(string email);
}
