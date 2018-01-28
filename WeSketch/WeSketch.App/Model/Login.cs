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
        public Login()
        {
            
        }

        public bool LoginByUsername(string username, string password)
        {
            var service = SketchService.GetService();
            User user = service.Login(username, password);
            Global.CurrentUser = user;
            return user.Id != -1;
        }
    }
}
