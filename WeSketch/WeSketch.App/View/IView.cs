using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Model;

namespace WeSketch.App.View
{
    public interface IView
    {
        void Display();
        void Init(ISketch model);
        void MakeController();
    }
}
