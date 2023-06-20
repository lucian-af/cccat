using Cccat.Autenticacao.Application.Models;
using Cccat.Autenticacao.Domain.Entities;
using Cccat.Autenticacao.Domain.Enums;
using Cccat.Autenticacao.Domain.Interfaces;

namespace Cccat.Autenticacao.Application.UseCases
{
	public class CadastraUsuario
	{
		private readonly IUsuarioRepository _usuarioRepository;

		public CadastraUsuario(IRepositoryFactory factory)
			=> _usuarioRepository = factory.CriarUsuarioRepository();

		public async Task<CadastraUsuarioOutputDto> Cadastrar(CadastraUsuarioInputDto input)
		{
			var usuario = Usuario.Criar(input.Email, input.Senha, SenhaTipo.Pbkdf2);
			await _usuarioRepository.Cadastrar(usuario);

			return new CadastraUsuarioOutputDto { IdUsuario = usuario.Id };
		}
	}
}
