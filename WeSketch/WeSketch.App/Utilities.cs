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
using System.Windows.Markup;
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
            await window.ShowMessageAsync(title, message);
        }

        public static string ExportShapes(ShapeList shapes)
        {
            //var shape = new ShapeRectangle(10, 200, System.Windows.Media.Color.FromRgb(0, 255, 0));
            //shapes.Add(shape);
            string xaml = XamlWriter.Save(shapes);//JsonConvert.SerializeObject(shape.MyElement, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            return xaml;
        }

        public static ShapeList ImportShapes(string data)
        {
            if (String.IsNullOrEmpty(data)) return new ShapeList();
            var shapes = (ShapeList)XamlReader.Parse(data);
            return shapes;
        }
    }
}
