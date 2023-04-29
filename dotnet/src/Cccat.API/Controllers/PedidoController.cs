using Cccat.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Text.Json;

namespace Cccat.API.Controllers
{
    [ApiController]
    [Route("checkout")]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "CriarPedido")]
        public IActionResult CriarPedido(dynamic request)
        {
            _logger.LogInformation("Buscando pedido...");

            var pedido = JsonSerializer.Deserialize<ExpandoObject>(request);

            if (ValidarCpf.Validar(pedido.cpf.ToString()))
            {
                var itens = JsonSerializer.Deserialize<List<ExpandoObject>>(pedido.itens.ToString());

                decimal total = 0;

                foreach (var item in itens)
                {
                    var produto = Produtos()
                        .FirstOrDefault(produto => produto.idProduto.ToString() == item.idProduto.ToString());

                    total += decimal.Parse(produto.valor.ToString()) * decimal.Parse(item.quantidade.ToString());
                }

                if (!string.IsNullOrWhiteSpace(pedido.cupom.ToString()))
                {
                    var cupomExistente = Cupons()
                        .FirstOrDefault(cupom => cupom.valor.ToString() == pedido.cupom.ToString());

                    if (cupomExistente is not null && Convert.ToDateTime(cupomExistente.data.ToString()) >= DateTime.Today)
                    {
                        total -= (total * (decimal)cupomExistente.percentual) / 100;
                    }
                }

                return Ok(new { total });
            }

            return BadRequest("CPF Inválido.");
        }

        private static IList<dynamic> Produtos()
        {
            return new List<dynamic>
            {
                new { idProduto = 1, valor = 1000M },
                new { idProduto = 2, valor = 5000M },
                new { idProduto = 3, valor = 90M },
                new { idProduto = 4, valor = 1500M },
                new { idProduto = 5, valor = 7000M },
            };
        }

        private static IList<dynamic> Cupons()
        {
            return new List<dynamic>
            {
                new { id = 1, valor = "VALE10", data = DateTime.Today.AddDays(-15), percentual = 10 },
                new { id = 2, valor = "VALE20", data = DateTime.Today.AddDays(15), percentual = 20 },
            };
        }
    }
}