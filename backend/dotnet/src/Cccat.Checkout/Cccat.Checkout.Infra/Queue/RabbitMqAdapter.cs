using System.Text;
using System.Text.Json;
using Cccat.Checkout.Application.Queue;
using Cccat.Checkout.Infra.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Cccat.Checkout.Infra.Queue;
public class RabbitMqAdapter : IQueue
{
	private readonly IModel _channel;
	private readonly RabbitMqSettings _settings;

	public RabbitMqAdapter(IOptions<RabbitMqSettings> settings)
	{
		_settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
		_channel = Conectar(_settings);
	}

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

	public void Publicar<T>(string nomeFila, T data)
	{
		_channel.QueueDeclare(nomeFila, durable: true, exclusive: false, autoDelete: false);
		_channel.QueueBind(nomeFila, _settings.Exchange, nomeFila);

		var payload = JsonSerializer.Serialize(data);
		_channel.BasicPublish(_settings.Exchange, nomeFila, body: Encoding.UTF8.GetBytes(payload));
	}
}
