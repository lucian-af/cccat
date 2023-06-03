using Cccat.Catalogo.Application.Factories;
using Cccat.Catalogo.Application.Models;
using Cccat.Catalogo.Application.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace Cccat.Catalogo.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class WebApiController : ControllerBase
    {
        private readonly ConsultaProdutos _consultaProdutos;
        private readonly ConsultaProduto _consultaProduto;

        public WebApiController(UseCaseFactory factory)
        {
            _consultaProdutos = factory.CriarConsultaProdutos();
            _consultaProduto = factory.CriarConsultaProduto();
        }

        [HttpGet]
        [Route("produtos")]
        public ActionResult<ConsultaProdutosOutputDto> ObterTodosProdutos()
        {
            var response = _consultaProdutos.Buscar();
            return Ok(response);
        }

        [HttpGet]
        [Route("produtos/{idProduto}")]
        public ActionResult<ConsultaProdutoOutputDto> ObterProdutoPorId(int idProduto)
        {
            var response = _consultaProduto.Buscar(idProduto);
            return Ok(response);
        }
    }
}