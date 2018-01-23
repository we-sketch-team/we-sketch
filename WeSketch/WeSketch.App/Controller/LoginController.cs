using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data.API;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
    public class LoginController : ILoginController
    {
        private ILogin login;
        private ILoginView view;

        private bool IsValid(string username, string password)
        {
            if (String.IsNullOrEmpty(username)) return false;
            if (String.IsNullOrEmpty(password)) return false;
            return true;
        }

        public void Init(ILogin model, ILoginView view)
        {
            login = model;
            this.view = view;
        }

        public void Login(string username, string password)
        {
            bool isValid = IsValid(username, password);
            if (!isValid)
            {
                view.LoginError();
                return;
            }

            bool success = login.LoginByUsername(username, password);
            if (success) view.LoginSuccess();
            else view.LoginError();
        }
    }
}
