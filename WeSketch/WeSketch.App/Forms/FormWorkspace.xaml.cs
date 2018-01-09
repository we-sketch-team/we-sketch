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
        private CustomDialog customDialog;
        private AddCollaboratorDialog addCollaborator;

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
            var board = model.GetBoard();
            board.Shapes = Utilities.ImportShapes(board.Content);
            board.MyCanvas = canvas;
            board.Draw(canvas);
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
            var board = model.GetBoard();
            var shapes = board.Shapes;
            string export = Utilities.ExportShapes(shapes);
            System.IO.File.WriteAllText("shapes.txt", export);
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            var xaml = System.IO.File.ReadAllText("shapes.txt");
            var shapes = Utilities.ImportShapes(xaml);
            var board = model.GetBoard();
            board.Shapes = shapes;
            board.Draw(canvas);
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
            if (String.IsNullOrEmpty(username)) return;
            if (controller.AddCollaborator(username))
            {
                LoadCollaborators();
                Utilities.DisplayMessage(this, "Ok", $"User with username: {username} added as collaborator.");
                this.HideMetroDialogAsync(customDialog);
            }
            else
            {
                Utilities.DisplayMessage(this, "Error", $"User with username: {username} not found!");
                addCollaborator.tbxCollaboratorUsername.Clear();
            }
        }

        private void BtnCancelAddCollaborator(object sender, RoutedEventArgs e)
        {
            this.HideMetroDialogAsync(customDialog);
        }

        private void LoadCollaborators()
        {
            var board = model.GetBoard();
            var collaborators = controller.GetCollaboratorList(board); // api get collaborators
            dataGridCollaborators.ItemsSource = collaborators.Collaborators;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCollaborators();
        }

        private void btnRemoveCollaborator_Click(object sender, RoutedEventArgs e)
        {
            User user = dataGridCollaborators.SelectedItem as User;
            if (user == null) return;
            controller.RemoveCollaborator(user);
            LoadCollaborators();
        }
    }
}
