using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.API;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
    public class DashboardController : IDashboardController
    {
        private IDashboard dashboard;
        private IDashboardView view;

        private bool IsValid(string title, string password)
        {
            if (String.IsNullOrEmpty(title)) return false;
            return true;
        }

        public void CreateBoard(string title, string password)
        {
            bool isValid = IsValid(title, password);
            if (!isValid)
            {
                view.BoardNotCreated();
                return;
            }

            bool success = dashboard.CreateBoard(title, password);
            if (success)
            {
                view.BoardCreated();
                view.RefreshMyBoards();
            }
            else view.BoardNotCreated();
        }

        public void DeleteBoard(Board board)
        {
            dashboard.DeleteBoard(board);
            view.BoardDeleted();
            view.RefreshMyBoards();
        }

        public void Init(IDashboard model, IDashboardView view)
        {
            dashboard = model;
            this.view = view;
        }
    }
}
