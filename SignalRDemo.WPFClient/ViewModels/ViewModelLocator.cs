using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDemo.WPFClient.ViewModels
{
    public class ViewModelLocator
    {
        public HubConnection HubConnection => new HubConnection("http://localhost:53721/hubs", useDefaultUrl: false);

        public ShellViewModel ShellViewModel => new ShellViewModel(HubConnection);
    }
}
