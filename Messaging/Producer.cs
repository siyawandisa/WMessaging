using System;
using RabbitMQ.Client;
using System.Text;

namespace Messaging
{
    public class Producer
    {        
        private static Producer instance = null;
        private static readonly object padlock = new object();
        private IConnection connection; 
        private ConnectionFactory factory;
        private IModel channel; 

        private Producer(string broker, string queueName)
        {
            factory = new ConnectionFactory() { HostName = broker };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        
            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
        
        public static Producer GetInstance(string broker, string queueName)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Producer(broker, queueName);
                }
                return instance;
            }
        }
        
        public void SendMessage(string message, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                routingKey: routingKey,
                                basicProperties: null,
                                body: body);
            Console.WriteLine("Message sent: {0}", message);
        }

        ~Producer()
        {
            channel.Close();
            connection.Close();
        }
    }
}
