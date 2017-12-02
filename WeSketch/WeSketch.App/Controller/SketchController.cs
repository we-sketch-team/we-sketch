using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
    class SketchController : ISketchController
    {
        private ISketch myModel;
        private IView myView;

        public void AddShape(IShape shape)
        {
            myModel.AddShape(shape);
        }

        public void Init(ISketch model, IView view)
        {
            myModel = model;
            myView = view;
        }

        public void InvokeUpdate()
        {
            //throw new NotImplementedException();
        }
    }
}
