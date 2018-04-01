using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Common.CommonClasses
{
	public class UpdateActionSyncerDataModifier : SyncerDataModifier
	{
		public UpdateActionSyncerDataModifier(SyncerData previousData) : base(previousData)
		{

		}

		public override void Modify(CommonBoard board)
		{
			CommonBoard isForDelete = data.BoardsToDelete.Find(x => x.Title == board.Title);

			if (isForDelete != null)
				return;

			CommonBoard isForCreate = data.BoardsToCreate.Find(x => x.Title == board.Title);
			
			if(isForCreate != null)
			{
				data.BoardsToCreate.Find(x => x.Title == board.Title).Content = board.Content;
				return;
			}

			CommonBoard isAlreadyUpdated = data.BoardsToUpdate.Find(x => x.Title == board.Title);

			if (isAlreadyUpdated != null)
			{
				data.BoardsToUpdate.Find(x => x.Title == board.Title).Content = board.Content;
				return;
			}

			data.BoardsToUpdate.Add(board);
		}
	}
}
