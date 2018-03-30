using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.BusinessLogic.Utilities
{
	public abstract class OfflineSyncer
	{
		public abstract void DeleteBoards();
		public abstract void CreateBoards();
		public abstract void UpdateExistingBoards();

		public void Sync()
		{
			this.DeleteBoards();
			this.CreateBoards();
			this.UpdateExistingBoards();
		}
	}
}
