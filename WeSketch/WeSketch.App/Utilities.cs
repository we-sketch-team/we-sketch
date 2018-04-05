using ExtendedXmlSerializer.Configuration;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App
{
    public static class Utilities
    {
        public static async void DisplayMessage(MetroWindow window, string title, string message)
        {
            await (window.Invoke(() => window.ShowMessageAsync(title, message)));
        }

        public static string ExportShapes(ShapeList shapes)
        {
            foreach (var shape in shapes)
            {
                var container = shape.GetFrameworkContainer();
                var elem = shape.GetFrameworkShape();
                elem.Width = container.Width;
                elem.Height = container.Height;
                var left = Canvas.GetLeft(container);
                var top = Canvas.GetTop(container);
                elem.Uid = left + "$" + top + "$" + elem.Fill.ToString();
                container.Content = elem;
            }
            string xaml = System.Xaml.XamlServices.Save(shapes);
            return xaml;
        }

        public static ShapeList ImportShapes(string data)
        {
            if (String.IsNullOrEmpty(data)) return new ShapeList();
            var shapes = (ShapeList)System.Xaml.XamlServices.Parse(data);
            foreach (var shape in shapes)
            {
                var container = shape.GetFrameworkContainer();
                var elem = shape.GetFrameworkShape();
                container.Width = elem.Width;
                container.Height = elem.Height;
                var info = elem.Uid;
                var parsed = info.Split('$');
                var left = Double.Parse(parsed[0]);
                var top = Double.Parse(parsed[1]);
                elem.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(parsed[2]));
                Canvas.SetLeft(container, left);
                Canvas.SetTop(container, top);
                elem.Width = elem.Height = Double.NaN;
                container.Content = elem;
            }
            return shapes;
        }
    }
}
