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
using System.Threading;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormWorkspace.xaml
    /// </summary>
    public partial class FormWorkspace : MetroWindow, IWorkspaceView, INotifySelectedToolChanged, IDrawable, IConnectionObserver
    {
        private IWorkspace workspace;
        private IWorkspaceController controller;
        private bool isInQueue = false;

        private CustomDialog customDialog;

        private Toolbar toolbar;
        private ITool selectedTool;

        private IShape selectedShape;
        private double scale = 1.0;

        public FormWorkspace(IWorkspace model=null)
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


            //foreach (var tool in toolbar.Tools)
            //{
            //    formToolbar.Items.Add(tool);
            //}

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
            this.controller = new GuestWorkspaceController();
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
            RefreshCollaborators();
            RefreshUserQueue();
            canvas.MouseUp += canvas_MouseUp;
            canvas.MouseDown += canvas_MouseDown;
        }

        public void RefreshCollaborators()
        {
            dataGridCollaborators.ItemsSource = workspace.LoadBoardCollaborators();
			dataGridCollaborators.IsReadOnly = true;
        }

        public void RefreshCanvas()
        {
            var board = workspace.GetBoard();
            board.MyCanvas = canvas;
            board.Draw(canvas);
            foreach(Control child in canvas.Children)
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
            //_propertyGrid.SelectedObject = shape.GetFrameworkShape();
        }

        private void canvas_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void MetroWindow_KeyUp(object sender, KeyEventArgs e)
        {
			//if (e.Key == Key.Delete)
			//    controller.DeleteShape(selectedShape);

			if (e.Key != Key.Delete)
				return;

			RemoveCollaborator();
        }

		private void RemoveCollaborator()
		{
			User selected = dataGridCollaborators.SelectedItem as User;

			if (selected == null)
				return;
		
			int workingBoardId = workspace.GetBoard().Id;

			if (!CanRemoveCollaborator(workingBoardId, selected.Id)) 
				return;

			workspace.RemoveCollaborator(selected);
			this.RefreshCollaborators();
		}		

		private bool CanRemoveCollaborator(int boardid, int userId)
		{
			int searchedId = 0;
			Board board = Global.CurrentUser.Boards.Find(x => x.Id == boardid);

			if (board == null)
				return false;

			searchedId = board.Id;
			return boardid == searchedId && userId != Global.CurrentUser.Id;
		}

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ConnectionNotifier.Instance.Detach(this);
            workspace?.Detach(this);
            workspace?.Dispose();
        }

        public void UpdateMessage(Message message)
        {
            string time = DateTime.Now.ToShortTimeString();
            string trimmedText = message.Text.Trim();
            string text = $"[{time}] {message.Sender}: {trimmedText}{Environment.NewLine}";
            tbxChatbox.AppendText(text);
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            string messageSender = Global.CurrentUser.Username;
            string text = tbxMessage.Text;
            controller.SendMessage(messageSender, text);
            tbxMessage.Clear();
            tbxMessage.Focus();
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
            var board = workspace.GetBoard();
            UsersQueueLabel.Content = board.UserQueue.ToString();
        }

        public void SetController(IWorkspaceController workspaceController)
        {
            this.controller = workspaceController;
            if (toolbar == null) return;

            toolbar.SelectedTool.SetController(this.controller);
            toolbar.Controller = this.controller;
        }

        private void QueueButton_Click(object sender, RoutedEventArgs e)
        {
            workspace.EnterQueue();
            RefreshUserQueue();
            QueueButton_Copy.IsEnabled = true;
            QueueButton.IsEnabled = false;
        }

        private void QueueButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            workspace.LeaveQueue();
            RefreshUserQueue();
            QueueButton_Copy.IsEnabled = false;
            QueueButton.IsEnabled = true;
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
                MessageBox.Show("Connection lost. You entered offline mode!");
                this.Close();
            });
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if(e.NewValue.HasValue)
                Global.SelectedColor = e.NewValue.Value;
        }
    }
}
