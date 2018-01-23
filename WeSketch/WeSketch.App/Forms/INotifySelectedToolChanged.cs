using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data.Tools.Toolbar;

namespace WeSketch.App.Forms
{
    public interface INotifySelectedToolChanged
    {
        void UpdateSelectedTool();
    }
}
