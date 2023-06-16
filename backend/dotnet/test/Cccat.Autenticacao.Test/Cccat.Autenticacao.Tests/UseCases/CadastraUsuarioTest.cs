using Cccat.Autenticacao.Application.Models;
using Cccat.Autenticacao.Application.UseCases;
using Cccat.Autenticacao.Domain.Interfaces;
using Cccat.Autenticacao.Domain.Services;
using Cccat.Autenticacao.Tests.Fixtures;

namespace Cccat.Autenticacao.Tests.UseCases
{
	[Collection(nameof(DatabaseFixtureCollection))]
	public class CadastraUsuarioTest
	{
		private readonly CadastraUsuario _cadastraUsuario;
		private readonly AutenticaUsuario _autenticaUsuario;
		private readonly IUsuarioRepository _usuarioRepository;

		public CadastraUsuarioTest(DatabaseFixture dbFixture)
		{
			var factory = new DatabaseRepositoryFactoryFixture(dbFixture.DbContext)
				.CriarRepositoryFactory();

			_usuarioRepository = factory.CriarUsuarioRepository();

			_cadastraUsuario = new(factory);
			_autenticaUsuario = new(factory);
		}

		[Trait("Cccat", "UseCases.Autenticacao.CadastrarUsuario")]
		[Fact]
		public async Task DeveCadastraUsuario()
		{
			var cadastraUsuarioInput = new CadastraUsuarioInputDto
			{
				Email = "teste@teste.com.br",
				Senha = "Abc@123"
			};

			var response = await _cadastraUsuario.Cadastrar(cadastraUsuarioInput);
			var usuario = await _usuarioRepository.ObterUsuarioPorEmail(cadastraUsuarioInput.Email);

			Assert.NotNull(response);
			Assert.NotNull(usuario);
			Assert.Equal(response.IdUsuario, usuario.Id);
			Assert.Equal(cadastraUsuarioInput.Email, usuario.Email.Valor);

			var autenticaUsuarioInput = new AutenticaUsuarioInputDto
			{
				Email = cadastraUsuarioInput.Email,
				Senha = cadastraUsuarioInput.Senha
			};

			var autenticacao = await _autenticaUsuario.Autenticar(autenticaUsuarioInput);

			Assert.NotNull(autenticacao);
			Assert.True(TokenJwt.Validar(autenticacao.Token));
		}
	}
}
