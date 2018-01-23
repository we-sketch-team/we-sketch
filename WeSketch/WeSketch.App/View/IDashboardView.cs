using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Model;

namespace WeSketch.App.View
{
    public interface IDashboardView
    {
        void Init(IDashboard model);
        void MakeController();
        void RefreshMyBoards();
        void BoardCreated();
        void BoardNotCreated();
        void BoardDeleted();
    }
}
