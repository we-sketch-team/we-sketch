using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeSketch.Common.CommonClasses;

namespace WeSketch.App.Data
{
	public abstract class Syncronizer
	{
		public abstract void SendUpdate(SyncerData data);		

		public void RefreshGlobalSyncData()
		{
			Global.Syncer.BoardsToCreate = new List<CommonBoard>();
			Global.Syncer.BoardsToUpdate = new List<CommonBoard>();
			Global.Syncer.BoardsToDelete = new List<CommonBoard>();
		}

		public void Sync()
		{
            MessageBox.Show("Sync is on");
            //return;
			SyncerData data = Global.Syncer;
			SendUpdate(data);
			RefreshGlobalSyncData();
		}
	}
}
