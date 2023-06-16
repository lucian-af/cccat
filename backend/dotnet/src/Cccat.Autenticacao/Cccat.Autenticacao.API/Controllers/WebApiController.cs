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

		public WebApiController(UseCaseFactory factory)
			=> _cadastraUsuario = factory.CriarCadastraUsuario();

		[HttpPost]
		[Route("autenticacao/cadastrar")]
		public async Task<IActionResult> CadastrarUsuario(CadastraUsuarioInputDto request)
		{
			var response = await _cadastraUsuario.Cadastrar(request);
			return Ok(response.IdUsuario);
		}
	}
}
