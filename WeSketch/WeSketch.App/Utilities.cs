using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App
{
    public static class Utilities
    {
        public static async void DisplayMessage(MetroWindow window, string title, string message)
        {
            await window.ShowMessageAsync(title, message);
        }
    }
}
