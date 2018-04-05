using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
    interface ILoginController
    {
        void Init(ILogin model, ILoginView view);
        void Login(string username, string password);
    }
}
