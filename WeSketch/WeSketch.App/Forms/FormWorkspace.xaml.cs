﻿using MahApps.Metro.Controls;
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

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormWorkspace.xaml
    /// </summary>
    public partial class FormWorkspace : MetroWindow, IView, IObserver
    {
        private RectangleCreationalTool rct;
        private ISketch model;
        private ISketchController controller;

        public FormWorkspace()
        {
            InitializeComponent();
            model = new Sketch();
            Init(model);
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
            Board b = new Board();
            b.MyCanvas = canvas;
            model.OpenBoard(b);
            model.Attach(this);
            MakeController();
        }

        public void MakeController()
        {
            this.controller = new SketchController();
            controller.Init(model, this);
            model.Attach(this.controller);
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
            rct.MouseDrag((int)point.X, (int)point.Y);
        }
    }
}
