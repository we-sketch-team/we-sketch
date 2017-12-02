using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WeSketch.App.Data.Shapes
{
    abstract class Shape: IShape
    {
        protected System.Windows.Shapes.Shape myElement;

        public Shape()
        {

        }

        public void Delete(Canvas target)
        {
            target.Children.Remove(myElement);
        }
        
        public void Draw(Canvas target)
        {
            target.Children.Add(myElement);
        }

        public void Move(int x, int y)
        {
            Canvas.SetLeft(myElement, x);
            Canvas.SetTop(myElement, y);
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
    }
}
