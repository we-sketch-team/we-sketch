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
using WeSketch.App.Controller;
using WeSketch.App.Data;
using WeSketch.App.Data.API;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormLogin.xaml
    /// </summary>
    public partial class FormLogin : MetroWindow, ILoginView
    {
        private ILogin login;
        private ILoginController controller;

        public FormLogin()
        {
            InitializeComponent();
            PopulateComboBoxWithServers();
            Init(new Login());
        }

        private void PopulateComboBoxWithServers()
        {
            cbxServer.Items.Add("http://localhost:15000/");
            cbxServer.Items.Add("http://10.10.0.5:15000/");
            cbxServer.Items.Add("http://160.99.38.140:15000/");

            cbxServer.SelectedIndex = 1;

            Global.ServerURI = cbxServer.SelectedItem as string;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Global.ServerURI = cbxServer.SelectedItem as string;
            //
            FormRegister reg = new FormRegister(this);
            reg.ShowDialog();            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Global.ServerURI = cbxServer.SelectedItem as string;
            //
            string username = tbxEmail.Text;
            string password = tbxPassword.Password;

            controller.Login(username, password);
        }

        public void Init(ILogin model)
        {
            login = model;
            MakeController();
        }

        public void MakeController()
        {
            controller = new LoginController();
            controller.Init(login, this);
        }

        public void LoginSuccess()
        {
            FormDashboard dash = new FormDashboard(new Dashboard());
            dash.ShowDialog();
        }

        public void LoginError()
        {
            Utilities.DisplayMessage(this, "Can't login", "Incorrect username or password!");
        }
    }
}
