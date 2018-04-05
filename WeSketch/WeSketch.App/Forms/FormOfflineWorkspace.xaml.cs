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
using System.Windows.Markup;
using MahApps.Metro.Controls.Dialogs;
using WeSketch.App.Dialogs;
using WeSketch.App.Data.Tools.Toolbar;
using WeSketch.App.Data.Shapes;
using WeSketch.Common;
using System.Threading;
using System.Windows.Media.Effects;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormOfflineWorkspace.xaml
    /// </summary>
    public partial class FormOfflineWorkspace : MetroWindow, IWorkspaceView, INotifySelectedToolChanged, IDrawable, IConnectionObserver
    {
        private IWorkspace workspace;
        private IWorkspaceController controller;
        private bool isInQueue = false;

        private CustomDialog customDialog;

        private Toolbar toolbar;
        private ITool selectedTool;

        private IShape selectedShape;
        private double scale = 1.0;

        public FormOfflineWorkspace(IWorkspace model = null)
        {
            InitializeComponent();
            if (model == null)
                return;
            ConnectionNotifier.Instance.Attach(this);
            Global.ResizeAndDragStyle = this.FindResource("DesignerItemTemplate") as ControlTemplate;
            Init(model);
            PopulateFormToolbar();
            //_propertyGrid.PropertyValueChanged += _propertyGrid_PropertyValueChanged;
            canvas.ClipToBounds = true;
            ColorPicker.SelectedColor = Global.SelectedColor;
        }

        private void _propertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            //Utilities.DisplayMessage(this, "A", "Data changed");
        }

        private void PopulateFormToolbar()
        {
            toolbar = new Toolbar(this);
            toolbar.Controller = controller;
            toolbar.Register(new SelectToolRepresent(this));
            toolbar.Register(new RectangleToolRepresent(toolbar));
            toolbar.Register(new EllipseToolRepresent(toolbar));
            toolbar.Register(new ZoomInToolRepresent(this));
            toolbar.Register(new ZoomOutToolRepresent(this));

            formToolbar.ItemsSource = toolbar.Tools;

            if (toolbar.Tools.Count > 0)
                toolbar.Select(toolbar.Tools[0]);

            selectedTool = toolbar.SelectedTool;
        }

        public void Init(IWorkspace model)
        {
            this.workspace = model;
            workspace.Attach(this);
            MakeController();
            RefreshCanvas();
        }

        public void MakeController()
        {
            this.controller = new OfflineWorkspaceController();
            controller.Init(workspace, this);
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(canvas);
            selectedTool.MouseDown((int)point.X, (int)point.Y);
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(canvas);
            selectedTool.MouseUp((int)point.X, (int)point.Y);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void btnEnableCT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            canvas.MouseUp += canvas_MouseUp;
            canvas.MouseDown += canvas_MouseDown;
        }

        public void RefreshCanvas()
        {
            var board = workspace.GetBoard();
            board.MyCanvas = canvas;
            board.Draw(canvas);
            foreach (Control child in canvas.Children)
            {
                child.Template = Global.ResizeAndDragStyle;
            }
        }

        public void UpdateSelectedTool()
        {
            selectedTool = toolbar.SelectedTool;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            workspace.SaveBoard();
        }

        public Canvas GetCanvas()
        {
            return canvas;
        }

        public void SelectShape(IShape shape)
        {
            selectedShape = shape;
        }

        private void canvas_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void MetroWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                controller.DeleteShape(selectedShape);
        }

       

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ConnectionNotifier.Instance.Detach(this);
            workspace?.Detach(this);
            workspace?.Dispose();
        }

        

        public void SetController(IWorkspaceController workspaceController)
        {
            this.controller = workspaceController;
            if (toolbar == null) return;

            toolbar.SelectedTool.SetController(this.controller);
            toolbar.Controller = this.controller;
        }

        

        private void MoveThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            var thumb = sender as MoveThumb;
            if (thumb == null) return;
            var control = thumb.DataContext as Control;
            controller.Drag(control, e.VerticalChange, e.HorizontalChange);
        }

        private void ResizeThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            var thumb = sender as ResizeThumb;
            if (thumb == null) return;
            var control = thumb.DataContext as Control;
            controller.Resize(control, e.VerticalChange, e.HorizontalChange, thumb.VerticalAlignment, thumb.HorizontalAlignment);
        }

        private void MoveThumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            controller.DragCompleted();
        }

        private void ResizeThumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            controller.ResizeCompleted();
        }

        public void UpdateConnectionStatus(bool hasConnection)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                MessageBox.Show("Connection is back. You are online now!");
                this.Close();
            });
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
                Global.SelectedColor = e.NewValue.Value;
        }

        public void RefreshCollaborators()
        {
            throw new NotImplementedException();
        }

        public void UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void RefreshUserQueue()
        {
            throw new NotImplementedException();
        }
    }
}
