using System;
using System.ServiceModel;

namespace StatisServiceHost
{
    class Program
    {
        static void Main()
        {
            //HandleDb4o.LoadTestData(HandleDb4o.StoreYapFileName);
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

            var db = HandleDb4o.Database;
            if(db != null)
            {
                db.Close();
            }

        }
    }
}
