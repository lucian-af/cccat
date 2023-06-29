namespace Cccat.Checkout.Application.Queue;
public interface IQueue
{
	public void Publicar<T>(string nomeFila, T data);
}
