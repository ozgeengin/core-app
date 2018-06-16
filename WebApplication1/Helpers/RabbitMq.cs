using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace WebApplication1.Helpers
{
    public static class RabbitMq
    {
        private static readonly ConnectionFactory Factory;

        static RabbitMq(){
            Factory = new ConnectionFactory() { HostName = Constants.RabbitMqHostName };
        }

        public static void SendToQueue(string queueName,string message)
        {
            using (var connection = Factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);
            }
        }

        public static List<string> ReceiveFromQueue(string queueName)
        {
            var results=new List<string>();
            using (var connection = Factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var queueResponse = channel.QueueDeclare(queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                for (var i = 0; i < queueResponse.MessageCount; i++)
                {
                    var result = channel.BasicGet(queueName, true);
                    if (result != null)
                        results.Add(Encoding.UTF8.GetString(result.Body));
                }
            }

            return results;
        }

    }
}
