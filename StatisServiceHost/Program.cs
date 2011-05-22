using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace StatisServiceHost
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting...");
            var crossDomainserviceHost = new ServiceHost(typeof(CrossDomainService));
            crossDomainserviceHost.Open();
            
            var serviceHost = new ServiceHost(typeof(QuestionnaireService));
            serviceHost.Open();
            
            Console.WriteLine("Started. cds");

            Console.ReadLine();
            Console.WriteLine("Closing...");
            serviceHost.Close();
            crossDomainserviceHost.Close();
            Console.WriteLine("Closed.");

        }
    }
}
