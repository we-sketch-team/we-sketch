using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for FormLogin.xaml
    /// </summary>
    public partial class FormLogin : MetroWindow
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            FormRegister reg = new FormRegister(this);
            reg.ShowDialog();
            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Utilities.DisplayMessage(this, "Can't login", "Not implemented yet!");
            FormDashboard dash = new FormDashboard();
            dash.Show();            
        }
    }
}
