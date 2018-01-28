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
            await (window.Invoke(() => window.ShowMessageAsync(title, message)));
        }

        public static string ExportShapes(ShapeList shapes)
        {
            string xaml = System.Xaml.XamlServices.Save(shapes);
            return xaml;
        }

        public static ShapeList ImportShapes(string data)
        {
            if (String.IsNullOrEmpty(data)) return new ShapeList();
            var shapes = (ShapeList)System.Xaml.XamlServices.Parse(data);
            return shapes;
        }
    }
}
