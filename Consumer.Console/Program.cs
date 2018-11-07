using System;
using Messaging;
using Utils;

namespace Consumer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Consumer console!");

            var settings = Helpers.GetMessagingSettings();            
            var consumer = Messaging.Consumer.GetInstance(settings.BrokerName, settings.RoutingKey);
            consumer.ReceiveMessages(settings.RoutingKey);
        }
    }
}
