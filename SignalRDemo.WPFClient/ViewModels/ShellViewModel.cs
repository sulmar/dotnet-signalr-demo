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
    // Install-Package Microsoft.AspNet.SignalR.Client
    public class ShellViewModel
    {
        private readonly HubConnection hubConnection;
        IHubProxy routesHubProxy;

        public RelayCommand SendCommand { get; set; }

        public ConnectionState ConnectionState => ConnectionState.Connected;

        public ObservableCollection<Customer> Customers { get; set; }

        public ShellViewModel(HubConnection hubConnection)
        {
            Customers = new ObservableCollection<Customer>();

            this.SendCommand = new RelayCommand(p => Send(), ()=>hubConnection.State == ConnectionState.Connected);

            this.hubConnection = hubConnection;

            hubConnection.Closed += HubConnection_Closed;
            hubConnection.Error += ex => Console.WriteLine("SignalR error: {0}", ex.Message);


            routesHubProxy = hubConnection.CreateHubProxy("CustomersHub");

            routesHubProxy.On<Customer>("broadcastCustomer", OnChangedCustomer);

            hubConnection.Start();

            hubConnection.StateChanged += HubConnection_StateChanged;
        }

        private void HubConnection_StateChanged(StateChange obj)
        {
          
            DispatchService.Invoke(()=>SendCommand.OnCanExecuteChanged());
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
            var customer = new Customer { Id = 99, FirstName = "John", LastName = "Smith", StartupDate = DateTime.Now };

            routesHubProxy.Invoke<Customer>("SendCustomer", customer);

        }






    }
}
