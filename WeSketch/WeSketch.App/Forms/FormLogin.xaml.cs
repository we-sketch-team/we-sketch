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
using WeSketch.App.Data;
using WeSketch.App.Data.API;
using WeSketch.App.Model;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormLogin.xaml
    /// </summary>
    public partial class FormLogin : MetroWindow
    {
        IAPI api;

        public FormLogin()
        {
            InitializeComponent();
            api = new SketchService();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            FormRegister reg = new FormRegister(this);
            reg.ShowDialog();
            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxEmail.Text;
            string password = tbxPassword.Password;

            User logged = api.Login(username, password);
            
            if(logged.Id != -1)
            {
                ISketch sketch = new Sketch();
                sketch.SetUser(logged);
                FormDashboard dash = new FormDashboard(sketch);
                dash.Show();
            }
            else
            {
                Utilities.DisplayMessage(this, "Can't login", "Incorrect username or password!");
            }          
        }
    }
}
