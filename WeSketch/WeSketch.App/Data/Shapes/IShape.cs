using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.Shapes
{
    public interface IShape
    {
        void Draw(System.Windows.Controls.Canvas target);
        void Delete(System.Windows.Controls.Canvas target);
        void Move(int x, int y);
        void SetLeft(int left);
        void SetRight(int right);
        void SetTop(int top);
        void SetBottom(int bottom);
    }
}
