using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Common.CommonClasses
{
	public class DeleteActionSyncerDataModifier : SyncerDataModifier
	{
		public DeleteActionSyncerDataModifier(SyncerData previousData) : base(previousData)
		{

		}

		public override void Modify(CommonBoard board)
		{

			CommonBoard isForCreate = data.BoardsToCreate.Find(x => x.Title == board.Title);

			if (isForCreate != null)
			{
				data.BoardsToCreate.Remove(isForCreate);
				return;
			}

			CommonBoard isAlreadyUpdated = data.BoardsToUpdate.Find(x => x.Title == board.Title);

			if (isAlreadyUpdated != null)
			{
				data.BoardsToUpdate.Remove(isAlreadyUpdated);
			}

			data.BoardsToDelete.Add(board);
		}
	}
}
