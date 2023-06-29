namespace Cccat.Estoque.BackgroundTask.Queue;
public interface IQueue
{
	public void Consumir<T>(string nomeFila, Action<T> act);
}
