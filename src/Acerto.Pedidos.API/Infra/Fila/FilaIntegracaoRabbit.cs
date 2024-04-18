using System.Text;
using System.Text.Json;
using Acerto.Pedidos.API.Dominio.Interfaces.Fila;
using RabbitMQ.Client;

namespace Acerto.Pedidos.API.Infra.Fila;

public class FilaIntegracaoRabbit : IFilaIntegracao
{
    public void EnviarMensagem<T>(T message)
    {

        var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();


        channel.QueueDeclare(queue: "orders",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "",
                                routingKey: "orders",
                                basicProperties: null,
                                body: body);

    }
}

