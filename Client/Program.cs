using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5003/test", HttpTransportType.WebSockets)
                .Build();

            do
            {
                connection.StartAsync().Wait();
                Task.Delay(TimeSpan.FromMilliseconds(500));
            } while (connection.State != HubConnectionState.Connected);


            connection.On("testCall", () =>
            {
                Console.WriteLine("Hello world");
            });

            Console.ReadKey();
        }
    }
}
