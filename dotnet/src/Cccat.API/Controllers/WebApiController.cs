using Cccat.UseCases;
using Cccat.UseCases.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cccat.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class WebApiController : ControllerBase
    {
        private readonly Checkout _checkout;
        private readonly ConsultaProduto _consultaProduto;

        public WebApiController(UseCaseFactory factory)
        {
            _checkout = factory.CriarCheckout();
            _consultaProduto = factory.CriarConsultaProduto();
        }

        [HttpPost]
        [Route("checkout")]
        public async Task<ActionResult<CheckoutOutputDto>> CriarPedido(CheckoutInputDto request)
        {
            try
            {
                var response = await _checkout.Executar(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("produtos")]
        public ActionResult<ConsultaProdutoOutputDto> ObterTodosProdutos()
        {
            var response = _consultaProduto.ObterTodos();
            return Ok(response);
        }
    }
}