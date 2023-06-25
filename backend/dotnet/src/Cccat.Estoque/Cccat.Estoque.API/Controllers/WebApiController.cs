using Cccat.Estoque.Application.Factories;
using Cccat.Estoque.Application.Models;
using Cccat.Estoque.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api")]
	public class WebApiController : ControllerBase
	{
		private readonly BaixaEstoque _baixaEstoque;
		private readonly AdicionaEstoque _adicionaEstoque;
		private readonly ConsultaEstoque _consultaEstoque;

		public WebApiController(UseCaseFactory factory)
		{
			_baixaEstoque = factory.CriarBaixaEstoque();
			_consultaEstoque = factory.CriarConsultaEstoque();
			_adicionaEstoque = factory.CriarAdicionaEstoque();
		}

		[HttpPost]
		[Route("baixar-estoque")]
		public async Task<IActionResult> BaixarEstoque(BaixaEstoqueInputDto request)
		{
			await _baixaEstoque.Baixar(request);
			return Ok();
		}

		[HttpPost]
		[Route("adicionar-estoque")]
		public async Task<IActionResult> AdicionarEstoque(AdicionaEstoqueInputDto request)
		{
			await _adicionaEstoque.Adicionar(request);
			return Ok();
		}

		[HttpGet]
		[Route("consultar-estoque/{idProduto}")]
		public async Task<ActionResult<ConsultaEstoqueOutputDto>> ConsultarEstoque(int idProduto)
		{
			var output = await _consultaEstoque.Consultar(idProduto);
			return Ok(output);
		}
	}
}
