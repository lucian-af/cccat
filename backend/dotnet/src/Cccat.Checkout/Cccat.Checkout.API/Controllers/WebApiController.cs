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

        public WebApiController(UseCaseFactory factory)
            => _checkout = factory.CriarCheckout();

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
    }
}