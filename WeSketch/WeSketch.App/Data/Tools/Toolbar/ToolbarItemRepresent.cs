using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeSketch.App.Data.Tools.Toolbar
{
    public abstract class ToolbarItemRepresent: Button
    {
        private ITool tool;
        private Toolbar toolbar;
        private Color activatedIndicator = Color.FromRgb(150, 150, 150);
        private Color deactivatedIndicator = Color.FromRgb(220, 220, 220);

        public ToolbarItemRepresent()
        {
            SetBackColor(deactivatedIndicator);
            this.Width = this.Height = 50;
            SetButtonIcon();
        }

        protected virtual void SetButtonIcon()
        {
            throw new NotImplementedException();
        }

        private void SetBackColor(Color color)
        {
            Brush b = new SolidColorBrush(color);
            this.Background = b;
        }


        public void AttachTool(ITool tool)
        {
            this.tool = tool;
            this.Click += ToolbarRepresent_Click;
        }

        public void SetToolbar(Toolbar toolbar)
        {
            this.toolbar = toolbar;
        }

        public ITool GetTool()
        {
            return tool;
        }

        private void ToolbarRepresent_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            toolbar.Select(this);
        }

        public void Activate()
        {
            tool.Activate();
            SetBackColor(activatedIndicator);
        }

        public void Deactivate()
        {
            tool.Deactivate();
            SetBackColor(deactivatedIndicator);
        }
    }
}
