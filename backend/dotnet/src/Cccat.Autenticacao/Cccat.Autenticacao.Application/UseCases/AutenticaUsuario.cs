using Cccat.Autenticacao.Application.Models;
using Cccat.Autenticacao.Domain.Interfaces;
using Cccat.Autenticacao.Domain.Services;

namespace Cccat.Autenticacao.Application.UseCases
{
	public class AutenticaUsuario
	{
		private readonly IUsuarioRepository _usuarioRepository;

		public AutenticaUsuario(IRepositoryFactory factory)
			=> _usuarioRepository = factory.CriarUsuarioRepository();

		public async Task<AutenticaUsuarioOutputDto> Autenticar(AutenticaUsuarioInputDto input)
		{
			var usuario = await _usuarioRepository.ObterUsuarioPorEmail(input.Email)
				?? throw new Exception("Usuário não encontrado.");

			if (!usuario.ValidarSenha(input.Senha))
				throw new Exception("Falha na autenticação.");

			var token = TokenJwt.Gerar(DateTime.Now.AddHours(2), new()
				{
					{ nameof(input.Email),  input.Email }
				});
			return new AutenticaUsuarioOutputDto { Token = token };
		}
	}
}
