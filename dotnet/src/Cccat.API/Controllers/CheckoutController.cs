using Cccat.UseCases;
using Cccat.UseCases.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cccat.API.Controllers
{
    [ApiController]
    [Route("checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly Checkout _checkout;

        public CheckoutController(Checkout checkout)
            => _checkout = checkout;

        [HttpPost]
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