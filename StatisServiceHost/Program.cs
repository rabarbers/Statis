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
            var serviceHost = new ServiceHost(typeof(QuestionnaireService));
            
            serviceHost.Open();
            Console.WriteLine("Started.");

            Console.ReadLine();
            Console.WriteLine("Closing...");
            serviceHost.Close();
            Console.WriteLine("Closed.");
        }
    }
}
