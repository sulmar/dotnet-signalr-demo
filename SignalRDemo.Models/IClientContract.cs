using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDemo.Models
{
    public interface IClientContract
    {
        void broadcastCustomer(Customer customer);
    }
}
