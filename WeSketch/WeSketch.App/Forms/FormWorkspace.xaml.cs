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
using WeSketch.App.Communications;
using WeSketch.App.Data.Tools.Toolbar;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormWorkspace.xaml
    /// </summary>
    public partial class FormWorkspace : MetroWindow, IWorkspaceView, INotifySelectedToolChanged, IDrawable
    {
        private IWorkspace workspace;
        private IWorkspaceController controller;

        private CustomDialog customDialog;
        private AddCollaboratorDialog addCollaborator;

        private Toolbar toolbar;
        private ITool selectedTool;

        private IShape selectedShape; // PropertySheet

        public FormWorkspace(IWorkspace model)
        {
            InitializeComponent();
            Init(model);    
            PopulateFormToolbar();
        }

        private void PopulateFormToolbar()
        {
            toolbar = new Toolbar(this);
            toolbar.Register(new SelectToolRepresent(this));
            toolbar.Register(new RectangleToolRepresent(controller));
            toolbar.Register(new EllipseToolRepresent(controller));
            

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
            MakeController();
            RefreshCanvas();
        }

        public void MakeController()
        {
            this.controller = new WorkspaceController();
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
            var point = e.GetPosition(canvas);
            //rct.MouseDrag((int)point.X, (int)point.Y);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //var board = model.GetBoard();
            //var shapes = board.Shapes;
            //string export = Utilities.ExportShapes(shapes);
            //System.IO.File.WriteAllText("shapes.txt", export);
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            //var xaml = System.IO.File.ReadAllText("shapes.txt");
            //var shapes = Utilities.ImportShapes(xaml);
            //var board = model.GetBoard();
            //board.Shapes = shapes;
            //board.Draw(canvas);
        }

        private void btnEnableCT_Click(object sender, RoutedEventArgs e)
        {
            canvas.MouseUp += canvas_MouseUp;
            canvas.MouseDown += canvas_MouseDown;
        }

        private void btnAddCollaborator_Click(object sender, RoutedEventArgs e)
        {
            AddCollaborator();
        }

        private async void AddCollaborator()
        {
            customDialog = new CustomDialog();
            addCollaborator = new AddCollaboratorDialog();
            addCollaborator.btnCancel.Click += BtnCancelAddCollaborator;
            addCollaborator.btnAdd.Click += BtnAddCollaborator;
            customDialog.Content = addCollaborator;
            await this.ShowMetroDialogAsync(customDialog);
        }

        private void BtnAddCollaborator(object sender, RoutedEventArgs e)
        {
            string username = addCollaborator.tbxCollaboratorUsername.Text;
            controller.AddCollaborator(username);
            this.HideMetroDialogAsync(customDialog);
        }

        private void BtnCancelAddCollaborator(object sender, RoutedEventArgs e)
        {
            this.HideMetroDialogAsync(customDialog);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshCollaborators();
        }

        private void btnRemoveCollaborator_Click(object sender, RoutedEventArgs e)
        {
            User user = dataGridCollaborators.SelectedItem as User;
            if (user == null) return;
            controller.RemoveCollaborator(user);
        }

        public void RefreshCollaborators()
        {
            dataGridCollaborators.ItemsSource = workspace.LoadBoardCollaborators().Collaborators;
        }

        public void RefreshCanvas()
        {
            var board = workspace.GetBoard();
            board.Shapes = Utilities.ImportShapes(board.Content);
            board.MyCanvas = canvas;
            board.Draw(canvas);
        }

        public void CollaboratorAdded()
        {
            Utilities.DisplayMessage(this, "Collaborator added", "Collaborator successfuly added!");
        }

        public void CollaboratorNotAdded()
        {
            Utilities.DisplayMessage(this, "Collaborator not added", "Check collaborator username");
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
            Utilities.DisplayMessage(this, "TEST", shape.GetType().Name);
        }
    }
}
