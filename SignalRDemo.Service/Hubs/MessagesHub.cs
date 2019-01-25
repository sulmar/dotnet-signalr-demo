using Microsoft.AspNet.SignalR;
using SignalRDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalRDemo.Service.Hubs
{
    public class MessagesHub : Hub
    {
        public void Send(string message)
        {
            Clients.Others.broadcastMessage(message);
        }

        public void SendCustomer(Customer customer)
        {
            Clients.Others.broadcastCustomer(customer);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }
}