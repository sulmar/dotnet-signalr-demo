using Microsoft.AspNet.SignalR;
using SignalRDemo.Models;
using System.Threading.Tasks;

namespace SignalRDemo.Service.Hubs
{
    // Przykład Huba z silnym typowaniem (na podstawie interfejsu)
    public class CustomersHub : Hub<IClientContract>
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public void SendCustomer(Customer customer)
        {
            Clients.Others.broadcastCustomer(customer);
        }


    }
}