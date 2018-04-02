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

namespace WeSketch.App.Forms
{
	/// <summary>
	/// Interaction logic for FormEditUser.xaml
	/// </summary>
	public partial class FormEditUser : Window
	{
		public FormEditUser()
		{
			InitializeComponent();
			tb_FirstName.Text = Global.CurrentUser.FirstName;
			tb_LastName.Text = Global.CurrentUser.LastName;
		}

		private void btn_Edit_Click(object sender, RoutedEventArgs e)
		{
			bool status = UpdateProfile();
			this.Close();
		}

		public bool UpdateProfile()
		{
			IAPI service = SketchService.GetService();
			string firstName = tb_FirstName.Text;
			string lastName = tb_LastName.Text;

			firstName = string.IsNullOrEmpty(firstName) ? Global.CurrentUser.FirstName : firstName;
			lastName = string.IsNullOrEmpty(lastName) ? Global.CurrentUser.LastName : lastName;

			User user = new User
			{
				FirstName = firstName,
				LastName = lastName
			};
			string password = tb_Password.Text;

			bool successs = service.UpdateUserProfile(user, password);

			return successs;
		}
	}
}
