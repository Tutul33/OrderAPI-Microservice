using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using MediatR;
using DTOs.Command;
using System.Text.Json.Serialization;

namespace InvApi.InterCom.RabbitMQListener
{
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly ConnectionFactory _factory;
        private readonly IMediator _mediator;
        private readonly ILogger<RabbitMqConsumerService> _logger;

        public RabbitMqConsumerService(IMediator mediator, ConnectionFactory factory, ILogger<RabbitMqConsumerService> logger)
        {
            _mediator = mediator;
            _factory = factory;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queues = new List<string> { "orderQueue", "itemQueue" };
            foreach (var queue in queues)
            {
                Task.Run(() => StartConsumer(queue, stoppingToken), stoppingToken);
            }
            return Task.CompletedTask;
        }

        private void StartConsumer(string queueName, CancellationToken stoppingToken)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation($"Received message from {queueName}");
                    string decodedMessage = System.Text.RegularExpressions.Regex.Unescape(message);
                    if (decodedMessage.StartsWith("\"") && decodedMessage.EndsWith("\""))
                    {
                        decodedMessage = decodedMessage.Trim('"');
                    }
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };
                    try
                    {
                        switch (queueName)
                        {
                            //case "orderQueue":
                            //    var orderCommand = JsonSerializer.Deserialize<CreateOrderCommand>(decodedMessage, options);
                            //    await _mediator.Send(orderCommand);
                            //    break;
                            case "itemQueue":
                                var inventoryCommand = JsonSerializer.Deserialize<CreateItemCommand>(decodedMessage, options);
                                await _mediator.Send(inventoryCommand);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing message from {queueName}");
                    }

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };

                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                stoppingToken.WaitHandle.WaitOne();
            }
        }
        //private async Task ProcessMessage<T>(string message) where T : IRequest
        //{
        //    // Decode Unicode escape sequences
        //    var decodedMessage = System.Text.RegularExpressions.Regex.Unescape(message);

        //    var command = JsonSerializer.Deserialize<T>(decodedMessage);
        //    if (command != null)
        //    {
        //        await _mediator.Send(command);
        //    }
        //    else
        //    {
        //        _logger.LogError($"Failed to deserialize message: {decodedMessage}");
        //    }
        //}
    }


}