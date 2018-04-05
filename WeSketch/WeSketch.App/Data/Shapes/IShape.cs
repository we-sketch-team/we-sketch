using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeSketch.App.Data.Shapes
{
    public interface IShape
    {
        void Draw(Canvas target);
        void Delete(Canvas target);
        void Move(int x, int y);
        void SetLeft(int left);
        void SetRight(int right);
        void SetTop(int top);
        void SetBottom(int bottom);
        void SetWidth(double width);
        void SetHeight(double height);
        double GetWidth();
        double GetHeight();
        System.Windows.Shapes.Shape GetFrameworkShape();
        ContentControl GetFrameworkContainer();
    }
}
