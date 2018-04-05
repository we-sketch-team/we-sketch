using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
    interface IDashboardController
    {
        void Init(IDashboard model, IDashboardView view);
        void CreateBoard(string title, string password);
        void DeleteBoard(Board board);
    }
}
