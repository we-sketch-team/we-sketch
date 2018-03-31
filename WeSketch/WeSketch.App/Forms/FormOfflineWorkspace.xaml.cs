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
using MahApps.Metro.Controls.Dialogs;
using WeSketch.App.Dialogs;
using WeSketch.App.Data.Tools.Toolbar;
using WeSketch.App.Data.Shapes;
using WeSketch.Common;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormWorkspace.xaml
    /// </summary>
    public partial class FormOfflineWorkspace : MetroWindow, IWorkspaceView, INotifySelectedToolChanged, IDrawable
    {
        private IWorkspace workspace;
        private IWorkspaceController controller;

        private CustomDialog customDialog;

        private Toolbar toolbar;
        private ITool selectedTool;

        private IShape selectedShape;


        public FormOfflineWorkspace(IWorkspace model)
        {
            InitializeComponent();
            Init(model);    
            PopulateFormToolbar();
            //_propertyGrid.PropertyValueChanged += _propertyGrid_PropertyValueChanged;
            canvas.ClipToBounds = true;
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
            
            foreach (var tool in toolbar.Tools)
            {
                formToolbar.Items.Add(tool);
            }

            if(toolbar.Tools.Count > 0)
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
            //
            //workspace.SaveBoard();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {            

        }

        private void btnEnableCT_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshCollaborators();
            RefreshUserQueue();
            canvas.MouseUp += canvas_MouseUp;
            canvas.MouseDown += canvas_MouseDown;
        }

        public void RefreshCollaborators()
        {
			return;
        }

        public void RefreshCanvas()
        {
            var board = workspace.GetBoard();
            board.Shapes = Utilities.ImportShapes(board.Content);
            board.MyCanvas = canvas;
            board.Draw(canvas);
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
            //_propertyGrid.SelectedObject = shape.GetFrameworkShape();
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
            workspace?.Detach(this);
            workspace?.Dispose();
        }

        public void UpdateMessage(Message message)
        {
			return;          
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
			return;
        }

        private void tbxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        public void RefreshUserQueue()
        {
			return;
        }

        public void SetController(IWorkspaceController workspaceController)
        {
            this.controller = workspaceController;
            if (toolbar == null) return;

            toolbar.SelectedTool.SetController(this.controller);
            toolbar.Controller = this.controller;
        }        
    }
}
