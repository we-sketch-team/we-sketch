using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App
{
    public interface IConnectionObserver
    {
        void UpdateConnectionStatus(bool hasConnection);
    }
}
