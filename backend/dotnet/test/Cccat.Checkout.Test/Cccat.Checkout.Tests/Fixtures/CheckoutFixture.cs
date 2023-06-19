﻿using Cccat.Checkout.Application.Factories;
using Cccat.Checkout.Application.Gateways;
using Cccat.Checkout.Application.Models;
using Cccat.Checkout.Infra.Factories;
using Cccat.Checkout.Infra.Gateways;
using Cccat.Checkout.Infra.HttpClients;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Cccat.Checkout.Tests.Fixtures
{
	public class CheckoutFixture
	{
		public IServiceProvider ServiceProvider { get; private set; }

		public CheckoutFixture()
			=> ServiceProvider = CriarServiceCollection();

		public IServiceProvider CriarServiceCollection()
		{
			var services = new ServiceCollection();
			services.AddScoped<IGatewayFactory, GatewayHttpFactory>();
			services.AddScoped<IFreteGateway, FreteHttpGateway>();
			services.AddScoped<ICatalogoGateway, CatalogoHttpGateway>();
			services
				.AddRefitClient<IFreteHttpClient>()
				.ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri("http://localhost:5102/api"));
			services
				.AddRefitClient<ICatalogoHttpClient>()
				.ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri("http://localhost:5101/api"));

			return services.BuildServiceProvider();
		}

		public CheckoutInputDto CriarInputValido()
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
				Cupom = "VALE20",
				CepOrigem = "17600090",
				CepDestino = "17602700"
			};

		public CheckoutInputDto CriarInputValidoSemCupomDesconto()
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

		public CheckoutInputDto CriarInputValidoSemFrete()
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
