using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WeSketch.App.Controller;
using WeSketch.App.Data;
using WeSketch.App.Data.API;
using WeSketch.App.Dialogs;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormDashboard.xaml
    /// </summary>
    public partial class FormDashboard : MetroWindow, IDashboardView
    {
        private IDashboard dashboard;
        private IDashboardController controller;

        private CreateBoardDialog createBoardDialog;
        private CustomDialog customDialog;

        public FormDashboard(IDashboard model)
        {
            InitializeComponent();
            Init(model);
        }

        private async void CreateBoard()
        {
            customDialog = new CustomDialog();
            createBoardDialog = new CreateBoardDialog();
            createBoardDialog.btnCancel.Click += ButtonCancel_Click; ;
            createBoardDialog.btnCreate.Click += ButtonCreate_Click; ;
            customDialog.Content = createBoardDialog;
            await this.ShowMetroDialogAsync(customDialog);
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            var title = createBoardDialog.tbxBoardTitle.Text;
            var pwd = createBoardDialog.pbxPassword.Password;
            this.HideMetroDialogAsync(customDialog);

            controller.CreateBoard(title, pwd);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.HideMetroDialogAsync(customDialog);
        }

        private void LoadMyBoards()
        {
            var boards = dashboard.GetCurrentUserBoardList();
            dataMyBoards.ItemsSource = boards;
        }

        private void LoadBoardsSharedWithMe()
        {
            var boards = dashboard.GetOtherBoards();
            dataSharedWithMe.ItemsSource = boards;
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshMyBoards();
        }

        private void dataMyBoards_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var table = sender as DataGrid;
            if (table == null) return;
            if (table.SelectedItem == null) return;

            // TODO: Password check!
            var board = table.SelectedItem as Board;
            IWorkspace workspace = new Workspace();
            workspace.SetBoard(board);
            FormWorkspace form = new FormWorkspace(workspace);
            form.Show();
        }

        private void btnCreateBoard_Click(object sender, RoutedEventArgs e)
        {
            CreateBoard();
        }

        private void btnDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            if (dataMyBoards.SelectedItem == null) return;
            // TODO: Check if can delete
            var board = dataMyBoards.SelectedItem as Board;
            controller.DeleteBoard(board);
        }

        public void RefreshMyBoards()
        {
            LoadMyBoards();
            LoadBoardsSharedWithMe();
        }

        public void MakeController()
        {
            controller = new DashboardController();
            controller.Init(dashboard, this);
        }

        public void Init(IDashboard model)
        {
            dashboard = model;
            MakeController();
        }

        public void BoardCreated()
        {
            Utilities.DisplayMessage(this, "Success!", "Board created!");
        }

        public void BoardNotCreated()
        {
            Utilities.DisplayMessage(this, "Error!", "Board not created!");
        }

        public void BoardDeleted()
        {
            Utilities.DisplayMessage(this, "OK!", "Board deleted!");
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadMyBoards();
        }
    }
}
