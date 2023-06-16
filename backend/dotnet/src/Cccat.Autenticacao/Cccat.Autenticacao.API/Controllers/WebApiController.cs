using Cccat.Autenticacao.Application.Factories;
using Cccat.Autenticacao.Application.Models;
using Cccat.Autenticacao.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Cccat.Autenticacao.API.Controllers
{
	[ApiController]
	[Route("api")]
	public class WebApiController : ControllerBase
	{
		private readonly CadastraUsuario _cadastraUsuario;
		private readonly AutenticaUsuario _autenticaUsuario;

		public WebApiController(UseCaseFactory factory)
		{
			_cadastraUsuario = factory.CriarCadastraUsuario();
			_autenticaUsuario = factory.CriarAutenticaUsuario();
		}

		[HttpPost]
		[Route("autenticacao/cadastrar")]
		public async Task<IActionResult> CadastrarUsuario(CadastraUsuarioInputDto request)
		{
			var response = await _cadastraUsuario.Cadastrar(request);
			return Ok(response.IdUsuario);
		}

		[HttpPost]
		[Route("autenticacao/autenticar")]
		public async Task<ActionResult<AutenticaUsuarioOutputDto>> AutenticarUsuario(AutenticaUsuarioInputDto request)
		{
			var response = await _autenticaUsuario.Autenticar(request);
			return Ok(response);
		}
	}
}
