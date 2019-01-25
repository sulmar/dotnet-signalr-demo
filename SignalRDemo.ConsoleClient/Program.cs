using Microsoft.AspNet.SignalR.Client;
using SignalRDemo.Models;
using System;
using System.Threading.Tasks;

namespace SignalRDemo.ConsoleClient
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();


      
        private static async Task MainAsync()
        {
            Console.WriteLine("SignalR Client started.");

           //  await SignalRClientTest();

            await SignalRStrongTypedClientTest();

            Console.WriteLine("Press any key exit.");
            Console.ReadKey();
        }


        // Install-Package Microsoft.AspNet.SignalR.Client
        private static async Task SignalRClientTest()
        {
            var hubConnection = new HubConnection("http://localhost:53721/hubs", useDefaultUrl: false);
            hubConnection.TraceLevel = TraceLevels.All;
            hubConnection.TraceWriter = Console.Out;

            IHubProxy routesHubProxy = hubConnection.CreateHubProxy("MessagesHub");
            

            routesHubProxy.On<string>("broadcastMessage", message => Console.WriteLine($"received {message}"));

            try
            {
                Console.WriteLine("connecting...");
                await hubConnection.Start();

                Console.WriteLine("connected.");

                string message;

                do
                {
                    Console.WriteLine("Type mesage or (q)uit");

                    message = Console.ReadLine();

                    Console.WriteLine("sending...");

                    await routesHubProxy.Invoke<string>("Send", message);

                    Console.WriteLine("sent.");
                }
                while (message != "q");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetError());
            }
        }


        private static async Task SignalRStrongTypedClientTest()
        {
            var hubConnection = new HubConnection("http://localhost:53721/hubs", useDefaultUrl: false);
            hubConnection.TraceLevel = TraceLevels.All;
            hubConnection.TraceWriter = Console.Out;

            IHubProxy routesHubProxy = hubConnection.CreateHubProxy("CustomersHub");
            routesHubProxy.On<Customer>("broadcastCustomer", customer => Console.WriteLine($"received {customer.FirstName}"));

            try
            {
                Console.WriteLine("connecting...");
                await hubConnection.Start();

                Console.WriteLine("connected.");

                string firstName;

                do
                {
                    Console.WriteLine("Type customer name or (q)uit");

                    firstName = Console.ReadLine();

                    Console.WriteLine("sending...");

                    var customer = new Customer { Id = 99, FirstName = firstName, LastName = "Smith", StartupDate = DateTime.Now };

                    await routesHubProxy.Invoke<Customer>("SendCustomer", customer);

                    Console.WriteLine("sent.");
                }
                while (firstName != "q");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetError());
            }
        }
    }
}
