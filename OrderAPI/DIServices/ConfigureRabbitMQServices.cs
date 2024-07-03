

using InvApi.InterCom.RabbitMQListener;
using RabbitMQ.Client;

namespace AdminService.DIServices
{
    public static class ConfigureRabbitMQServices
    {
        public static void Register(WebApplicationBuilder builder)
        {

            builder.Services.AddSingleton(new ConnectionFactory
            {
                HostName = builder.Configuration["RabbitMQ:HostName"],
                UserName = builder.Configuration["RabbitMQ:UserName"],
                Password = builder.Configuration["RabbitMQ:Password"],
                Port = int.Parse(builder.Configuration["RabbitMQ:Port"])
            });
            builder.Services.AddHostedService<RabbitMqConsumerService>();
        }
    }
}
