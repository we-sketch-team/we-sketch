using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
    public interface ISketchController:IObserver
    {
        void Init(ISketch model, IView view);

        void AddShape(IShape shape);
    }
}
