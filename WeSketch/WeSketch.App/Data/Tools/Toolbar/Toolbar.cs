using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;
using WeSketch.App.Data.Tools;
using WeSketch.App.Forms;

namespace WeSketch.App.Data.Tools.Toolbar
{
    public class Toolbar
    {
        private List<ToolbarItemRepresent> tools;
        private ToolbarItemRepresent selectedTool;
        private INotifySelectedToolChanged form;

        public List<ToolbarItemRepresent> Tools { get { return tools; } }
    
        public Toolbar(INotifySelectedToolChanged form)
        {
            tools = new List<ToolbarItemRepresent>();
            this.form = form;
        }

        public ITool SelectedTool
        {
            get
            {
                return selectedTool.GetTool();
            }
        }

        public void Register(ToolbarItemRepresent tool)
        {
            tools.Add(tool);
            tool.SetToolbar(this);
        }

        public void Select(ToolbarItemRepresent tool)
        {
            if(selectedTool != null)
                selectedTool.Deactivate();
            selectedTool = tool;
            selectedTool.Activate();
            form.UpdateSelectedTool();
        }
    }
}
