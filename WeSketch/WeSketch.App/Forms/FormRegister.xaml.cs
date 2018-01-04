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
using WeSketch.App.Data.API;

namespace WeSketch.App.Forms
{
    /// <summary>
    /// Interaction logic for FormRegister.xaml
    /// </summary>
    public partial class FormRegister : MetroWindow
    {
        private MetroWindow parent;

        IAPI api;

        public FormRegister(MetroWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.parent.Visibility = Visibility.Hidden;
            api = new SketchService();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string email = tbxEmail.Text;
            string password = tbxPassword.Password;
            string fname = tbxFirstName.Text;
            string lname = tbxLastName.Text;

            UserRegistrationOptions options = new UserRegistrationOptions() { Username = username, Email = email, Password = password, FirstName = fname, LastName = lname };

            bool registered = api.Register(options);

            if(registered)
            {
                Utilities.DisplayMessage(this, "Success!", "You have registered, you can login now");
            }
            else
            {
                Utilities.DisplayMessage(this, "Error!", "You can't create account, email or username in use");
            }
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
