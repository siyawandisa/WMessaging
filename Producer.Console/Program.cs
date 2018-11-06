using System;
using System.Text;
using Messaging;
using Utils;

namespace Producer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Producer console!");
            
            var settings = Helpers.GetMessagingSettings();
            var producer = new Messaging.Producer(settings.BrokerName, settings.RoutingKey);
            var name = System.Console.ReadLine();
            var message = $"Hello my name is, {name}";
            System.Console.WriteLine("sending message: {0}", message);
            producer.SendMessage(message, settings.RoutingKey);
        }
    }
}
