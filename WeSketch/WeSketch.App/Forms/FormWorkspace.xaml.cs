using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeSketch.App.Data;
using WeSketch.App.Data.Tools;
using WeSketch.App.View;
using WeSketch.App.Model;
using WeSketch.App.Controller;
using System.Windows.Markup;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormWorkspace.xaml
    /// </summary>
    public partial class FormWorkspace : MetroWindow, IView
    {
        private RectangleCreationalTool rct;
        private ISketch model;
        private ISketchController controller;

        public FormWorkspace(ISketch sketch)
        {
            InitializeComponent();
            Init(sketch);
            rct = new RectangleCreationalTool();
            rct.SetController(controller);
        }

        public void Display()
        {
            //throw new NotImplementedException();
        }

        public void Init(ISketch model)
        {
            this.model = model;
            model.GetBoard().MyCanvas = canvas;
            MakeController();
        }

        public void MakeController()
        {
            this.controller = new SketchController();
            controller.Init(model, this);
        }

        public void InvokeUpdate()
        {
            //throw new NotImplementedException();
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(canvas);
            rct.MouseDown((int)point.X, (int)point.Y);
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(canvas);
            rct.MouseUp((int)point.X, (int)point.Y);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {            
            var point = e.GetPosition(canvas);
            //rct.MouseDrag((int)point.X, (int)point.Y);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string export = Utilities.ExportShapes(model.GetBoard());
            System.IO.File.WriteAllText("shapes.txt", export);
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            var xaml = System.IO.File.ReadAllText("shapes.txt");
            var shapes = Utilities.ImportShapes(xaml);
            model.GetBoard().Shapes = shapes;
            model.GetBoard().Draw(canvas);
        }
    }
}
