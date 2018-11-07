using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Message.Processor;

namespace Messaging
{
    public class Consumer
    {
        private static Consumer instance = null;
        private static readonly object padlock = new object();
        private IConnection connection; 
        private ConnectionFactory factory;
        private IModel channel; 

        private Consumer(string broker, string queueName)
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

        public static Consumer GetInstance(string broker, string queueName)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Consumer(broker, queueName);
                }
                return instance;
            }
        }

        public void ReceiveMessages(string routeKey)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Message Received: {0}", message);
                var name = Processor.GetName(message);
                Console.WriteLine("Hello {0}, I am your father.", name);                
            };
            channel.BasicConsume(queue: routeKey,
                                autoAck: true,
                                consumer: consumer);
        }

        ~Consumer()
        {    
            Console.WriteLine("Destroying Consumer ...");
            
            channel.Close();
            connection.Close();
        }
    }
}