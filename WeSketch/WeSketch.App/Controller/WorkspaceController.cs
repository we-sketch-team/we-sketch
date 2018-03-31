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
using WeSketch.Common;

namespace WeSketch.App.Controller
{
    public class WorkspaceController : IWorkspaceController
    {
        private IWorkspace workspace;
        private IWorkspaceView view;

        public void AddShape(IShape shape)
        {
            workspace.AddShape(shape);
        }

        public void DeleteShape(IShape selectedShape)
        {
            workspace.DeleteShape(selectedShape);
        }

        public void Drag(Control control, double verticalOffset, double horizontalOffset)
        {
            if (control == null)
                return;

            double left = Canvas.GetLeft(control);
            double top = Canvas.GetTop(control);

            Canvas.SetLeft(control, left + horizontalOffset);
            Canvas.SetTop(control, top + verticalOffset);
        }

        public void Init(IWorkspace model, IWorkspaceView view)
        {
            this.workspace = model;
            this.view = view;
            view.SetController(this);
        }

        public void Resize(Control control, double verticalOfset, double horizontalOffset, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment)
        {
            if (control == null)
                return;
            
            double deltaVertical, deltaHorizontal;

            switch (verticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    deltaVertical = Math.Min(-verticalOfset, control.ActualHeight - control.MinHeight);
                    control.Height -= deltaVertical;
                    break;
                case VerticalAlignment.Top:
                    deltaVertical = Math.Min(verticalOfset, control.ActualHeight - control.MinHeight);
                    Canvas.SetTop(control, Canvas.GetTop(control) + deltaVertical);
                    control.Height -= deltaVertical;
                    break;
                default:
                    break;
            }

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    deltaHorizontal = Math.Min(horizontalOffset, control.ActualWidth - control.MinWidth);
                    Canvas.SetLeft(control, Canvas.GetLeft(control) + deltaHorizontal);
                    control.Width -= deltaHorizontal;
                    break;
                case HorizontalAlignment.Right:
                    deltaHorizontal = Math.Min(-horizontalOffset, control.ActualWidth - control.MinWidth);
                    control.Width -= deltaHorizontal;
                    break;
                default:
                    break;
            }
            
        }

        public virtual void SendMessage(string sender, string text)
        {
            if (String.IsNullOrEmpty(text)) return;

            Message message = new Message()
            {
                Sender = sender,
                Text = text,
                BoardId = workspace.GetBoard().Id
            };

            workspace.SendMessage(message);
        }
    }
}
