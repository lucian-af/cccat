using Cccat.Checkout.Application.Factories;
using Cccat.Checkout.Application.Gateways;
using Cccat.Checkout.Application.Models;
using Cccat.Checkout.Infra.Factories;
using Cccat.Checkout.Infra.Gateways;
using Cccat.Checkout.Infra.Handlers;
using Cccat.Checkout.Infra.HttpClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Cccat.Checkout.Tests.Fixtures
{
	public class CheckoutFixture
	{
		public IServiceProvider ServiceProvider { get; private set; }

		public CheckoutFixture()
			=> ServiceProvider = CriarServiceCollection();

		public static IServiceProvider CriarServiceCollection()
		{
			var services = AdicionarDependencias();
			return services.BuildServiceProvider();
		}

		private static ServiceCollection AdicionarDependencias()
		{
			var config = CriarConfiguracao();

			var services = new ServiceCollection();
			services.AddScoped<AutenticacaoHttpGateway>();
			services.AddScoped(typeof(AutenticacaoHandler), c
				=> new AutenticacaoHandler(c.GetRequiredService<AutenticacaoHttpGateway>(), config));
			services.AddScoped<HttpClientFactory>();
			services.AddScoped<IGatewayFactory, GatewayHttpFactory>();
			services.AddScoped<IFreteGateway, FreteHttpGateway>();
			services.AddScoped<ICatalogoGateway, CatalogoHttpGateway>();
			services.AddScoped<IEstoqueGateway, EstoqueHttpGateway>();
			services
				.AddRefitClient<IFreteHttpClient>()
				.ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri("http://localhost:5102/api"))
				.AddHttpMessageHandler<AutenticacaoHandler>();

			services
				.AddRefitClient<ICatalogoHttpClient>()
				.ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri("http://localhost:5101/api"))
				.AddHttpMessageHandler<AutenticacaoHandler>();

			services
				.AddRefitClient<IEstoqueHttpClient>()
				.ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri("http://localhost:5105/api"))
				.AddHttpMessageHandler<AutenticacaoHandler>();

			services
				.AddRefitClient<IAutenticacaoHttpClient>()
				.ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri("http://localhost:5104/api"));
			return services;
		}

		private static IConfiguration CriarConfiguracao()
		{
			var inMemorySettings = new Dictionary<string, string> {
				{"Token", "<um-super-segredo>"},
				{"UserSettings:Email", "cccat@cccat.com"},
				{"UserSettings:Senha", "c!c@c#@t"}
			};

			IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
			return config;
		}

		public CheckoutInputDto CriarInputValidoSomenteItens()
			=> new CheckoutInputDto
			{
				IdPedido = Guid.NewGuid(),
				Cpf = "407.302.170-27",
				Items = new()
				{
					new() { IdProduto = 1, Quantidade = 1 },
					new () { IdProduto = 2, Quantidade = 1 },
					new () { IdProduto = 3, Quantidade = 3 }
				}
			};

		public CheckoutInputDto CriarInputValidoComCupom()
			=> new CheckoutInputDto
			{
				IdPedido = Guid.NewGuid(),
				Cpf = "407.302.170-27",
				Items = new()
				{
					new() { IdProduto = 1, Quantidade = 1 },
					new () { IdProduto = 2, Quantidade = 1 },
					new () { IdProduto = 3, Quantidade = 3 }
				},
				Cupom = "VALE20"
			};

		public CheckoutInputDto CriarInputValidoComFrete()
			=> new CheckoutInputDto
			{
				IdPedido = Guid.NewGuid(),
				Cpf = "407.302.170-27",
				Items = new()
				{
					new() { IdProduto = 1, Quantidade = 1 },
					new () { IdProduto = 2, Quantidade = 1 },
					new () { IdProduto = 3, Quantidade = 3 }
				},
				CepOrigem = "17600090",
				CepDestino = "17602700"
			};

		public CheckoutInputDto CriarInputValidoComCupomEFrete()
			=> new CheckoutInputDto
			{
				IdPedido = Guid.NewGuid(),
				Cpf = "407.302.170-27",
				Items = new()
				{
					new() { IdProduto = 1, Quantidade = 1 },
					new () { IdProduto = 2, Quantidade = 1 },
					new () { IdProduto = 3, Quantidade = 3 }
				},
				CepOrigem = "17600090",
				CepDestino = "72980000",
				Cupom = "VALE20"
			};
	}
}
