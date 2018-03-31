using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
    public interface IWorkspaceController
    {
        void Init(IWorkspace model, IWorkspaceView view);
        void AddShape(IShape shape);
        void SendMessage(string sender, string text);
        void DeleteShape(IShape selectedShape);
        void Drag(Control control, double verticalOffset, double horizontalOffset);
        void Resize(Control control, double verticalOfset, double horizontalOffset, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment);
    }
}
