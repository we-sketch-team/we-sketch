﻿using MahApps.Metro.Controls;
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
        private JoinPasswordProtectedBoardDialog joinBoard;
        private CustomDialog customDialog = new CustomDialog();

        public FormDashboard(IDashboard model)
        {
            InitializeComponent();
            Init(model);
        }

        private async void CreateBoard()
        {
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
            dataMyBoards.Items.Refresh();
        }

        private void LoadBoardsSharedWithMe()
        {
            var boards = dashboard.GetOtherBoards();
            dataSharedWithMe.ItemsSource = boards;
            dataSharedWithMe.Items.Refresh();
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

            var board = table.SelectedItem as Board;
            if (board == null) return;
            IWorkspace workspace = new Workspace();
            workspace.SetBoard(board);
            var view = ViewManager.Instance.GetView(workspace);
            view.ShowDialog();
        }

        private void btnCreateBoard_Click(object sender, RoutedEventArgs e)
        {
            CreateBoard();
        }

        private void btnDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            if (dataMyBoards.SelectedItem == null) return;      
			
            var board = dataMyBoards.SelectedItem as Board;

			if (board == null)
				return;

			var belongsToUser = Global.CurrentUser.Boards.Find(x => x.Id == board.Id);

			if (belongsToUser == null)
				return;

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
			RefreshMyBoards();			
        }

        private void dataSharedWithMe_MouseDoubleClickAsync(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var table = sender as DataGrid;
            if (table == null) return;
            if (table.SelectedItem == null) return;

            // TODO: Password check!
            var board = table.SelectedItem as Board;
            Join(board);
        }

        private async void Join(Board board)
        {
            string password = "";
            bool success = false;
            if (board.IsPasswordProtected)
            {
                joinBoard = new JoinPasswordProtectedBoardDialog();
                joinBoard.btnCancel.Click += (s, args) =>
                {
                    this.HideMetroDialogAsync(customDialog);
                };
                joinBoard.btnJoin.Click += (s, args) =>
                {
                    password = joinBoard.PasswordBox.Password;
                    success = true;
                    this.HideMetroDialogAsync(customDialog);
                };
                customDialog.Content = joinBoard;
                await this.ShowMetroDialogAsync(customDialog);
                await customDialog.WaitUntilUnloadedAsync();
                if (!success) return;
            }
            
            if (!dashboard.JoinBoard(board, password))
            {
                Utilities.DisplayMessage(this, "Error", "Can't join");
                return;
            }
            IWorkspace workspace = new Workspace();
            workspace.SetBoard(board);
            FormWorkspace form = new FormWorkspace(workspace);
            form.ShowDialog();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
			FormEditUser editUser = new FormEditUser();
			editUser.ShowDialog();
        }
    }
}
