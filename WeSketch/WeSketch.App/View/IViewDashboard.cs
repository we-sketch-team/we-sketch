using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Model;

namespace WeSketch.App.View
{
    interface IViewDashboard
    {
        void UpdateMyBoards();
        void Init(ISketch model);
        void MakeController();
    }
}
