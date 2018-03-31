using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
	public class OfflineWorkspaceController : WorkspaceController
	{
		public override void SendMessage(string sender, string text)
		{
			return;
		}
	}
}
