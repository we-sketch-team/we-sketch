using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WeSketch.Common.CommonClasses;
using System.Windows.Threading;
using System.IO;
using System.Windows;

namespace WeSketch.App.Data
{
	public class DatabaseSyncronizer : Syncronizer
	{
		private string ServerURI = Global.ServerURI;
		private HubConnection connection;
		IHubProxy boardHub;

		public DatabaseSyncronizer()
		{
			connection = new HubConnection(ServerURI);			
			connection.TraceLevel = TraceLevels.All;
			ServicePointManager.DefaultConnectionLimit = 10;
			boardHub = connection.CreateHubProxy("BoardHub");
			connection.Start().Wait();
		}

		public override void SendUpdate(SyncerData data)
		{
			Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
			{
				boardHub.Invoke("SyncOfflineModeChanges", data);
			}));
		}
	}
}
