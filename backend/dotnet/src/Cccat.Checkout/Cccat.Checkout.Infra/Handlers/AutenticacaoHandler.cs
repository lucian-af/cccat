using Cccat.Checkout.Infra.Gateways;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace Cccat.Checkout.Infra.Handlers;

public class AutenticacaoHandler : DelegatingHandler
{
	private readonly AutenticacaoHttpGateway _httpGateway;
	private readonly IConfiguration _configuration;

	public AutenticacaoHandler(AutenticacaoHttpGateway httpGateway, IConfiguration configuration)
	{
		_httpGateway = httpGateway;
		_configuration = configuration;
	}

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var responseAutenticacao = await _httpGateway.Autenticar(new()
		{
			Email = _configuration.GetSection("UserSettings:Email").Value,
			Senha = _configuration.GetSection("UserSettings:Senha").Value
		});

		request.Headers.Authorization = new(JwtBearerDefaults.AuthenticationScheme, responseAutenticacao.Token);

		return await base.SendAsync(request, cancellationToken);
	}
}
