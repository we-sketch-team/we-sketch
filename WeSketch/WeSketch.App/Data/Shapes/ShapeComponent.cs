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
        
        public ShapeComponent()
        {
            
        }

        public void Delete(Canvas target)
        {
            target.Children.Remove(MyElement);
        }
        
        public void Draw(Canvas target)
        {
            target.Children.Add(MyElement);
        }

        public void Move(int x, int y)
        {
            Canvas.SetLeft(MyElement, x);
            Canvas.SetTop(MyElement, y);
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
            MyElement.Width = width;
        }

        public void SetHeight(double height)
        {
            MyElement.Height = height;
        }

        public double GetWidth()
        {
            return MyElement.Width;
        }

        public double GetHeight()
        {
            return MyElement.Height;
        }
    }
}
