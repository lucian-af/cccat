using System.Text.Json;
using Cccat.Estoque.Application.Factories;
using Cccat.Estoque.Application.Models;
using Cccat.Estoque.BackgroundTask.Mensagens;
using Cccat.Estoque.BackgroundTask.Queue;
using Microsoft.Extensions.DependencyInjection;

namespace Cccat.Estoque.BackgroundTask;
public class ProcessaEstoqueService : BackgroundService
{
	private readonly IQueue _queue;
	private readonly IServiceProvider _provider;

	public ProcessaEstoqueService(IServiceProvider provider)
	{
		_provider = provider;
		using var scope = _provider.CreateScope();
		_queue = scope.ServiceProvider.GetRequiredService<IQueue>();
	}

	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		cancellationToken.Register(() => Console.WriteLine($"Serviço: {nameof(ProcessaEstoqueService)} parado."));

		_queue.Consumir<PedidoRealizado>("pedidoRealizado", async message =>
		{
			try
			{
				Console.WriteLine($"Processando mensagem: {JsonSerializer.Serialize(message)}");
				var estoque = new BaixaEstoqueInputDto();
				foreach (var item in message.Itens)
				{
					estoque.Itens.Add(new() { IdProduto = item.IdProduto, Quantidade = item.Quantidade });
				}
				await BaixarEstoque(estoque);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
			}
			Console.WriteLine("Mensagem processada.");
			Console.WriteLine("Aguardando novas mensagens...");
		});

		while (!cancellationToken.IsCancellationRequested)
		{
			Console.WriteLine($"Serviço: {nameof(ProcessaEstoqueService)} ativo em: {DateTime.Now:G}");
			await Task.Delay(90000, cancellationToken);
		}
	}

	private async Task BaixarEstoque(BaixaEstoqueInputDto estoque)
	{
		var scope = _provider.CreateScope();
		var factory = scope.ServiceProvider.GetRequiredService<UseCaseFactory>();
		var baixaEstoque = factory.CriarBaixaEstoque();
		await baixaEstoque.Baixar(estoque);
	}
}
