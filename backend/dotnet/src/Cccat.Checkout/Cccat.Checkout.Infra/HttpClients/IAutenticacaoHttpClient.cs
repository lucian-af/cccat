using Cccat.Checkout.Infra.HttpClients.Dtos;
using Refit;

namespace Cccat.Checkout.Infra.HttpClients
{
	public interface IAutenticacaoHttpClient
	{
		[Post("/autenticacao/autenticar")]
		public Task<AutenticacaoResponseDto> Autenticar([Body] AutenticacaoDto dados);
	}
}
