using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Common.CommonClasses
{
	public class SyncerDataModifier
	{
		protected SyncerData data;

		public SyncerDataModifier(SyncerData previousData)
		{
			this.data = previousData;			
		}

		public virtual void Modify(CommonBoard board)
		{

		}	

		public SyncerData GetModifiedData()
		{
			return data;
		}		
	}
}
