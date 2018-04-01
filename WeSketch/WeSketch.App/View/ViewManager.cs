using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Forms;
using WeSketch.App.Model;

namespace WeSketch.App.View
{
    public enum Mode
    {
        Offline,
        Online
    };

    public class ViewManager: IConnectionObserver
    {
        public static Mode Mode { get; set; }
        private static ViewManager viewManager;

        public static ViewManager Instance
        {
            get
            {
                if (viewManager == null)
                    viewManager = new ViewManager();
                return viewManager;
            }
        }

        private ViewManager()
        {
            ConnectionNotifier.Instance.Attach(this);
            SetMode(ConnectionNotifier.Instance.HasConnection);
        }

        private void SetMode(bool hasConnection)
        {
            if (hasConnection)
                Mode = Mode.Online;
            else
                Mode = Mode.Offline;
        }

        public IWorkspaceView GetView(IWorkspace model)
        {
            IWorkspaceView view = null; // NullObject

            switch (Mode)
            {
                case Mode.Online:
                    view = new FormWorkspace(model);
                    break;
                case Mode.Offline:
                    view = new FormOfflineWorkspace(model);
                    break;
            }
            return view;
        }

        public void UpdateConnectionStatus(bool hasConnection)
        {
            SetMode(hasConnection);
        }
    }
}
