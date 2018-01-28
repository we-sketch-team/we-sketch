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
    public partial class Server : Form, ILoggable
    {
        public IDisposable SignalR { get; set; }
        string ServerURI;

        public Server()
        {
            InitializeComponent();
            Logger.Target = this;
        }

        private void StartServer()
        {
            var ip = tbxIP.Text;
            var port = tbxPort.Text;

            ServerURI = $"http://{ip}:{port}";
            try
            {
                SignalR = WebApp.Start(ServerURI);
            }
            catch (TargetInvocationException ex)
            {
                WriteToConsole("Server failed to start. A server is already running on " + ServerURI);
                //Re-enable button to let user try to start server again 
                this.Invoke((Action)(() => btnStart.Enabled = true));
                WriteToConsole(ex.Message);
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

        public void WriteToConsole(string message)
        {
            if (tbxLog.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                    WriteToConsole(message)
                ));
                return;
            }
            tbxLog.AppendText($"[{DateTime.Now.ToLongTimeString()}] {message}{Environment.NewLine}");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            WriteToConsole("Starting server...");
            btnStart.Enabled = false;
            Task.Run(() => StartServer());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            WriteToConsole("Stopping server...");
            btnStop.Enabled = false;
            StopServer();
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        public void Log(string message)
        {
            WriteToConsole(message);
        }
    }
}
