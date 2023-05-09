using Cccat.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Cccat.API.Controllers
{
    [ApiController]
    [Route("checkout")]
    public class PedidoController : ControllerBase
    {
        private readonly Checkout _checkout;

        public PedidoController(Checkout checkout)
            => _checkout = checkout;

        [HttpPost(Name = "CriarPedido")]
        public IActionResult CriarPedido(Input request)
        {
            try
            {
                var response = _checkout.Execute(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}