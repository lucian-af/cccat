namespace Cccat.Autenticacao.Domain.Interfaces
{
	public interface IRepositoryFactory
	{
		public IUsuarioRepository CriarUsuarioRepository();
	}
}
