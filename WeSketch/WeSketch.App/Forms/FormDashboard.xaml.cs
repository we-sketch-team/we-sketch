using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WeSketch.App.Data;
using WeSketch.App.Data.API;
using WeSketch.App.Model;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormDashboard.xaml
    /// </summary>
    public partial class FormDashboard : MetroWindow
    {
        ISketch sketch;
        IAPI api;

        public FormDashboard(ISketch sketch)
        {
            InitializeComponent();
            this.sketch = sketch;
            api = new SketchService();
        }

        private void LoadMyBoards()
        {
            var boards = api.GetMyBoards(sketch.GetUser());
            //boards.Boards[0].Collaborators.Add(new User() { Username = "Siska" });
            //boards.Boards[0].Collaborators.Add(new User() { Username = "Miska" });
            dataMyBoards.ItemsSource = boards.Boards;
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMyBoards();
        }

        private void dataMyBoards_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dataMyBoards.SelectedItem == null) return;
            var board = dataMyBoards.SelectedItem as Board;
            sketch.SetBoard(board);
            FormWorkspace form = new FormWorkspace(sketch);
            form.Show();
        }
    }
}
