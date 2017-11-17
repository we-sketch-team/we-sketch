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

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormRegister.xaml
    /// </summary>
    public partial class FormRegister : MetroWindow
    {
        private MetroWindow parent;

        public FormRegister(MetroWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.parent.Visibility = Visibility.Hidden;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Utilities.DisplayMessage(this, "Can't register", "Not implemented yet!");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.parent.Visibility = Visibility.Visible;
        }
    }
}
