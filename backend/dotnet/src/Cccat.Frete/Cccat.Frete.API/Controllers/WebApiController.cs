using Cccat.Frete.Application.Factories;
using Cccat.Frete.Application.Models;
using Cccat.Frete.Application.UseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cccat.Frete.API.Controllers
{
	[ApiController]
	[Route("api")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class WebApiController : ControllerBase
	{
		private readonly SimulaFrete _simulaFrete;

		public WebApiController(UseCaseFactory factory)
			=> _simulaFrete = factory.CriarSimulaFrete();

		[HttpPost]
		[Route("simularfrete")]
		public ActionResult<SimulaFreteOutputDto> CriarPedido(SimulaFreteInputDto request)
		{
			try
			{
				var response = _simulaFrete.Simular(request);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
