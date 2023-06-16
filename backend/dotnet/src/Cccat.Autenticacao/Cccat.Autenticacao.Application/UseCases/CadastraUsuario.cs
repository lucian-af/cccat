using Cccat.Autenticacao.Application.Models;
using Cccat.Autenticacao.Domain.Entities;
using Cccat.Autenticacao.Domain.Interfaces;

namespace Cccat.Autenticacao.Application.UseCases
{
	public class CadastraUsuario
	{
		private readonly IUsuarioRepository _usuarioRepository;

		public CadastraUsuario(IRepositoryFactory factory)
			=> _usuarioRepository = factory.CriarUsuarioRepository();

		public async Task<Output> Cadastrar(CadastraUsuarioInputDto input)
		{
			var usuario = new Usuario(input.Email, input.Senha);
			await _usuarioRepository.Cadastrar(usuario);

			return new Output { IdUsuario = usuario.Id };
		}
	}

	public class Output
	{
		public Guid IdUsuario { get; set; }
	}
}
