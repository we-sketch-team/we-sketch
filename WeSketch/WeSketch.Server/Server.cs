using Microsoft.AspNet.SignalR;
//using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeSketch.Server
{
    public partial class Server : Form
    {
        public IDisposable SignalR { get; set; }
        const string ServerURI = "http://localhost:15000";

        public Server()
        {
            InitializeComponent();
        }

        private void StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerURI);
            }
            catch (TargetInvocationException)
            {
                WriteToConsole("Server failed to start. A server is already running on " + ServerURI);
                //Re-enable button to let user try to start server again 
                this.Invoke((Action)(() => btnStart.Enabled = true));
                return;
            }
            this.Invoke((Action)(() => btnStop.Enabled = true));
            WriteToConsole("Server started at " + ServerURI);
        }

        private void StopServer()
        {
            if (SignalR != null)
            {
                SignalR.Dispose();
            }
        }

        private void WriteToConsole(string message)
        {
            if (tbxLog.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                    WriteToConsole(message)
                ));
                return;
            }
            tbxLog.AppendText($"[{DateTime.Now.ToShortDateString()}] {message}{Environment.NewLine}");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            WriteToConsole("Starting server...");
            btnStart.Enabled = false;
            Task.Run(() => StartServer());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }
    }
}
