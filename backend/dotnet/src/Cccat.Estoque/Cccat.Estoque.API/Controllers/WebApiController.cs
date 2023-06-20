using Cccat.Estoque.Application.Factories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api")]
	public class WebApiController : ControllerBase
	{
		public WebApiController(UseCaseFactory factory)
		{
		}

		[HttpGet]
		[Route("get")]
		public IActionResult Get() => Ok();
	}
}
