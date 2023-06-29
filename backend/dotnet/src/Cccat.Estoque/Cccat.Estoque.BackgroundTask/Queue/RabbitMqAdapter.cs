using System.Text;
using System.Text.Json;
using Cccat.Estoque.BackgroundTask.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cccat.Estoque.BackgroundTask.Queue;
public class RabbitMqAdapter : IQueue
{
	private readonly IModel _channel;

	public RabbitMqAdapter(IOptions<RabbitMqSettings> settings)
		=> _channel = Conectar(settings?.Value);

	private static IModel Conectar(RabbitMqSettings settings)
	{
		if (settings is null)
			throw new ArgumentNullException(nameof(settings));

		var _connection = new ConnectionFactory
		{
			HostName = settings.HostName,
			VirtualHost = "/",
			UserName = settings.UserName,
			Password = settings.Password,
		}.CreateConnection("cccat");

		var channel = _connection.CreateModel();
		channel.ExchangeDeclare(settings.Exchange, ExchangeType.Topic, durable: true, autoDelete: false);
		return channel;
	}

	public void Consumir<T>(string nomeFila, Action<T> act)
	{
		var consumer = new EventingBasicConsumer(_channel);

		consumer.Received += (sender, eventArgs) =>
		{
			var contentArray = eventArgs.Body.ToArray();
			var contentString = Encoding.UTF8.GetString(contentArray);
			var mensagem = JsonSerializer.Deserialize<T>(contentString);
			act(mensagem);
			_channel.BasicAck(eventArgs.DeliveryTag, false);
		};

		_channel.BasicConsume(nomeFila, false, consumer);
	}
}
