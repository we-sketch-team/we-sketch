using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WeSketch.App.Data.Shapes
{
    [Serializable]
    public class ShapeRectangle: ShapeComponent
    {
        public ShapeRectangle(int width, int height, System.Windows.Media.Color color) : base()
        {
            MyElement = new System.Windows.Shapes.Rectangle
            {
                Fill = new SolidColorBrush(color),
                IsHitTestVisible = false,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Tag = this
            };

            Container = new ContentControl()
            {
                Width = width,
                Height = height,
                Content = MyElement,
                Template = Application.Current.FindResource("DesignerItemTemplate") as ControlTemplate
            };            
        }

        public ShapeRectangle():this(0, 0, System.Windows.Media.Color.FromRgb(0,0,0))
        {
            
        }
    }
}
