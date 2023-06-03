namespace Cccat.Queue.Consumer
{
    //public class Worker : BackgroundService
    //{
    //    private readonly ILogger<Worker> _logger;
    //    private readonly IModel _channel;
    //    private readonly string _queue;
    //    private readonly Checkout _checkout;

    //    public Worker(ILogger<Worker> logger, Checkout checkout)
    //    {
    //        _logger = logger;
    //        _checkout = checkout;

    //        var _connection = new ConnectionFactory
    //        {
    //            HostName = "localhost",
    //            VirtualHost = "/",
    //            UserName = "guest",
    //            Password = "guest",
    //        }.CreateConnection("cccat");

    //        _logger.LogInformation("Configurando exchange...");
    //        _channel = _connection.CreateModel();
    //        var _exchange = "cccat_ex";
    //        _channel.ExchangeDeclare(_exchange, ExchangeType.Topic, durable: true, autoDelete: false);
    //        _logger.LogInformation("Configurando queue...");
    //        _queue = "cccat/checkout";
    //        _channel.QueueDeclare(_queue, durable: true, exclusive: false, autoDelete: false);
    //        _channel.QueueBind(_queue, _exchange, _queue);
    //    }

    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

    //        _logger.LogInformation("Recebendo mensagens...");
    //        await Task.Delay(1000, stoppingToken);
    //        var consumer = new EventingBasicConsumer(_channel);

    //        consumer.Received += async (sender, eventArgs) =>
    //        {
    //            var contentArray = eventArgs.Body.ToArray();
    //            var contentString = Encoding.UTF8.GetString(contentArray);
    //            var mensagem = JsonSerializer.Deserialize<CheckoutInputDto>(contentString);
    //            _logger.LogInformation($"mensagem recebida: {JsonSerializer.Serialize(mensagem)}");

    //            try
    //            {
    //                var result = _checkout.Executar(mensagem);
    //                _logger.LogInformation($"output: {JsonSerializer.Serialize(result)}");
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex.Message);
    //            }

    //            _channel.BasicAck(eventArgs.DeliveryTag, false);
    //        };

    //        _channel.BasicConsume(_queue, false, consumer);
    //    }
    //}
}