using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.API;

namespace WeSketch.App.Model
{
    public class Login : ILogin
    {
        private IAPI api;

        public Login()
        {
            api = new SketchService();
        }

        public bool LoginByUsername(string username, string password)
        {
            User user = api.Login(username, password);
            Global.CurrentUser = user;
            return user.Id != -1;
        }
    }
}
