using Cccat.Checkout.Application.Factories;
using Cccat.Checkout.Application.Models;
using Microsoft.AspNetCore.Mvc;
using US = Cccat.Checkout.Application.UseCase;

namespace Cccat.Checkout.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class WebApiController : ControllerBase
    {
        private readonly US.Checkout _checkout;
        private readonly US.ConsultaProduto _consultaProduto;

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