using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;
using WeSketch.App.Model;

namespace WeSketch.App.View
{
    class WSViewGuest : IView
    {
        private ISketch myModel;
        private SketchController myController;

        public void Display()
        {
            //throw new NotImplementedException();
        }

        public void Init(ISketch model)
        {
            myModel = model;
            MakeController();
        }

        public void MakeController()
        {
            myController = new SketchController();
            myController.Init(myModel, this);
        }

    }
}
