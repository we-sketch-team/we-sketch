using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeSketch.App
{
    public class ConnectionNotifier
    {
        public bool HasConnection { get; private set; }
        private static ConnectionNotifier notifier;
        private List<IConnectionObserver> observers;

        public static ConnectionNotifier Instance
        {
            get
            {
                if (notifier == null)
                    notifier = new ConnectionNotifier();
                return notifier;
            }
        }

        private ConnectionNotifier()
        {
            HasConnection = new Ping().Send("8.8.8.8").Status == IPStatus.Success; // Replace with real server ip
            observers = new List<IConnectionObserver>();
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
        }

        public void Attach(IConnectionObserver obs)
        {
            if (observers.Contains(obs))
                return;
            observers.Add(obs);
        }

        public void Detach(IConnectionObserver obs)
        {
            if (!observers.Contains(obs))
                return;

             observers.Remove(obs);
        }

        private void Notify()
        {
            observers.ForEach(o => Task.Factory.StartNew(()=>
            {
                Thread.Sleep(100);
                o.UpdateConnectionStatus(HasConnection);
            }
            ));
        }

        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            HasConnection = e.IsAvailable;
            Notify();
        }
    }
}
