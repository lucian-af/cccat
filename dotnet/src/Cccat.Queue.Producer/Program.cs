using Cccat.Application.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

Console.WriteLine("Criando conexão...");
var _connection = new ConnectionFactory
{
    HostName = "localhost",
    VirtualHost = "/",
    UserName = "guest",
    Password = "guest",
}.CreateConnection("cccat");

Console.WriteLine("Configurando exchange...");
var _channel = _connection.CreateModel();
var _queue = "cccat/checkout";
_channel.QueueDeclare(_queue, durable: true, exclusive: false, autoDelete: false);
var _exchange = "cccat_ex";
_channel.ExchangeDeclare(_exchange, ExchangeType.Topic, durable: true, autoDelete: false);
_channel.QueueBind(_queue, _exchange, _queue);

do
{
    Console.WriteLine("Criando payload...");
    var pedido = new CheckoutInputDto
    {
        Cpf = "407.302.170-27",
        Items = new()
        {
            new() { IdProduto = 1, Quantidade = 1 },
            new() { IdProduto = 2, Quantidade = 1 },
            new() { IdProduto = 3, Quantidade = 3 }
        },
        Cupom = "",
        CepOrigem = "",
        CepDestino = ""
    };

    Console.WriteLine("Enviando payload...");
    var payload = JsonSerializer.Serialize(pedido);
    _channel.BasicPublish(_exchange, _queue, body: Encoding.UTF8.GetBytes(payload));

    Console.WriteLine("Payload enviado.");
} while (Console.ReadLine() == "r");

Console.WriteLine("Serviço parado.");
Console.ReadLine();