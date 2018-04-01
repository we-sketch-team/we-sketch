using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Common.CommonClasses
{
	public class SyncerData
	{
		public List<CommonBoard> BoardsToDelete { get; set; }
		public List<CommonBoard> BoardsToUpdate { get; set; }
		public List<CommonBoard> BoardsToCreate { get; set; }

		public SyncerData()
		{
			BoardsToDelete = new List<CommonBoard>();
			BoardsToUpdate = new List<CommonBoard>();
			BoardsToCreate = new List<CommonBoard>();			
		}
	}
}
