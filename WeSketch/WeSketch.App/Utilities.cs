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

        public static string ExportShapes(Board board)
        {
            var shapes = board.Shapes;
            string str = "";
            str = XamlWriter.Save(shapes);//JsonConvert.SerializeObject(shape.MyElement, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            return str;
        }

        public static ShapeList ImportShapes(string data)
        {
            //var deserializedObject = JsonConvert.DeserializeObject<System.Windows.Shapes.Rectangle>(data, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            var shapes = (ShapeList)XamlReader.Parse(data);
            return shapes;
        }
    }
}
