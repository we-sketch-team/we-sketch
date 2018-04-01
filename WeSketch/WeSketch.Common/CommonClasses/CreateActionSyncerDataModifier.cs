using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Common.CommonClasses
{
	public class CreateActionSyncerDataModifier : SyncerDataModifier
	{
		public CreateActionSyncerDataModifier(SyncerData previousData) : base(previousData)
		{
				
		}

		public override void Modify(CommonBoard board)
		{
			data.BoardsToCreate.Add(board);			
		}
	}
}
