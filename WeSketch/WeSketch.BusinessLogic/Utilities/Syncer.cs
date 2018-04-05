using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.Providers;
using WeSketch.Common.CommonClasses;

namespace WeSketch.BusinessLogic.Utilities
{
	public class Syncer : OfflineSyncer
	{
		private BoardProvider boardProvider;
		private SyncerData data;

		public Syncer(SyncerData dataReceived)
		{
			boardProvider = ObjectFactory.GetBoardProvider(new Mediator());
			data = dataReceived;
		}

		public override void CreateBoards()
		{
			boardProvider.SyncCreatedBoards(data.BoardsToCreate);
		}

		public override void DeleteBoards()
		{
			boardProvider.SyncDeletedBoards(data.BoardsToDelete);
		}

		public override void UpdateExistingBoards()
		{
			boardProvider.SyncUpdatedBoards(data.BoardsToUpdate);
		}
	}
}
