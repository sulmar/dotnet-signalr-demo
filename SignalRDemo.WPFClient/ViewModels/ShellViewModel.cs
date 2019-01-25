using Microsoft.AspNet.SignalR.Client;
using SignalRDemo.Models;
using SignalRDemo.WPFClient.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SignalRDemo.WPFClient.ViewModels
{
    // Install-Package Microsoft.AspNetCore.SignalR.Client
    public class ShellViewModel
    {
        private readonly HubConnection hubConnection;
        IHubProxy routesHubProxy;

        public ICommand SendCommand { get; set; }


        public ObservableCollection<Customer> Customers { get; set; }

        public ShellViewModel(HubConnection hubConnection)
        {
            Customers = new ObservableCollection<Customer>();

            this.SendCommand = new RelayCommand(p => Send());

            this.hubConnection = hubConnection;

            hubConnection.Closed += HubConnection_Closed;


            routesHubProxy = hubConnection.CreateHubProxy("CustomersHub");

            routesHubProxy.On<Customer>("broadcastCustomer", OnChangedCustomer);

            hubConnection.Start();
        }

        
        private void OnChangedCustomer(Customer customer)
        {
            DispatchService.Invoke(()=>Customers.Add(customer));

        }

        private async void HubConnection_Closed()
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);

            await hubConnection.Start();
        }

        private void Send()
        {
            Customer customer = new Customer { Id = 1, FirstName = "Company 1" };

            routesHubProxy.Invoke<Customer>("SendCustomer", customer);

        }






    }
}
