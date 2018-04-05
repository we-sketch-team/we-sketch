using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WeSketch.App.Data.Shapes
{
    [Serializable]
    public abstract class ShapeComponent: IShape
    {
        public System.Windows.Shapes.Shape MyElement { get; set; }
        protected ContentControl Container { get; set; }

        public ShapeComponent()
        {
            
        }

        public void Delete(Canvas target)
        {
            target.Children.Remove(Container);
        }
        
        public void Draw(Canvas target)
        {
            target.Children.Add(Container);
        }

        public void Move(int x, int y)
        {
            Canvas.SetLeft(Container, x);
            Canvas.SetTop(Container, y);
        }

        public void Rotate(Canvas target, double angle)
        {
            throw new NotImplementedException();
        }

        public void SetBottom(int bottom)
        {
            throw new NotImplementedException();
        }

        public void SetLeft(int left)
        {
            throw new NotImplementedException();
        }

        public void SetRight(int right)
        {
            throw new NotImplementedException();
        }

        public void SetTop(int top)
        {
            throw new NotImplementedException();
        }

        public void Translate(Canvas target, int offsetX, int offsetY)
        {
            throw new NotImplementedException();
        }

        public void SetWidth(double width)
        {
            Container.Width = width;
        }

        public void SetHeight(double height)
        {
            Container.Height = height;
        }

        public double GetWidth()
        {
            return Container.Width;
        }

        public double GetHeight()
        {
            return Container.Height;
        }

        public Shape GetFrameworkShape()
        {
            return MyElement;
        }

        public ContentControl GetFrameworkContainer()
        {
            return Container;
        }
    }
}
