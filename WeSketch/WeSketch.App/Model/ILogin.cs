using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;

namespace WeSketch.App.Model
{
    public interface ILogin
    {
        bool LoginByUsername(string username, string password);
    }
}
