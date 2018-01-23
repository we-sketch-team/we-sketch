using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using WeSketch.App.Data;
using WeSketch.App.View;
using System.Windows.Threading;

namespace WeSketch.App.Communications
{
    class BoardCommunicator
    {
        /*IView view;*/
        private IHubProxy hubProxy;

        public BoardCommunicator(/*IView view*/)
        {
            /*this.view = view;*/
        }

        public async void ConnectToBoardHub()
        {
            var hubConnection = new HubConnection("http://localhost:55000/");
            hubProxy = hubConnection.CreateHubProxy("BoardHub");
            //hubProxy.On<Board>("ContentChangeNotification", board =>
            //            view.ShowMessage($"Board title: {board.Title} - Board content: {board.Content}"));
            await hubConnection.Start();
        }

        public async void SendUpd()
        {
            await hubProxy.Invoke("SendUpdate", new { Id = "3", Content = "promena stanja" });
        }
    }
}
