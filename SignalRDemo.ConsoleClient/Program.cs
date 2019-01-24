using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDemo.ConsoleClient
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();


      
        private static async Task MainAsync()
        {
            Console.WriteLine("SignalR Client started.");

            await SignalRClientTest();

            Console.WriteLine("Press any key exit.");
            Console.ReadKey();
        }

        // Install-Package Microsoft.AspNet.SignalR.Client
        private static async Task SignalRClientTest()
        {
            var hubConnection = new HubConnection("http://localhost:53721/hubs", useDefaultUrl: false);

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
    }
}
