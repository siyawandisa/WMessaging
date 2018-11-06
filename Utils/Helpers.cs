using System;
using System.Configuration;

namespace Utils
{
    public static class Helpers
    {        
        public static MessagingSettings GetMessagingSettings()
		{
			var routingKey = "localhost";
			var brokerName = "";

			try {
				routingKey = ConfigurationManager.AppSettings["RoutingKey"];
                brokerName = ConfigurationManager.AppSettings["BrokerName"];			
			}
			catch (Exception ex)
			{
                System.Console.WriteLine("error reading configuration file: {0}", ex.Message);                
			}
			return new MessagingSettings
				{
					RoutingKey = routingKey,
					BrokerName = brokerName
				};
		}
    }
}
