using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace OrderAPI.Common
{
    public static class PublishMessages
    {
        public static void Publish<T>(string queueName, T message)
        {
            var _factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }
    }
}
