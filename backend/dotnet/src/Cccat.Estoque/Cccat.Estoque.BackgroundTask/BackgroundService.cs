using Microsoft.Extensions.Hosting;

namespace Cccat.Estoque.BackgroundTask;

public abstract class BackgroundService : IHostedService, IDisposable
{
	private Task _executingTask;
	private readonly CancellationTokenSource _stoppingCts = new();

	protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

	public Task StartAsync(CancellationToken cancellationToken)
	{
		_executingTask = ExecuteAsync(cancellationToken);

		if (_executingTask.IsCompleted)
			return _executingTask;

		return Task.CompletedTask;
	}

	public virtual async Task StopAsync(CancellationToken cancellationToken)
	{
		if (_executingTask == null)
			return;

		try
		{
			_stoppingCts.Cancel();
		}
		finally
		{
			await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
			_stoppingCts.Cancel();
	}
}
