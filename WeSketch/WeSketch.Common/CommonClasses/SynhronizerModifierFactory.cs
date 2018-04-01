using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Common.CommonClasses
{
	public static class SynhronizerModifierFactory
	{
		public static SyncerDataModifier GetCreateActionModifier(SyncerData data)
		{
			return new CreateActionSyncerDataModifier(data); 
		}

		public static SyncerDataModifier GetUpdateActionModifier(SyncerData data)
		{
			return new UpdateActionSyncerDataModifier(data);
		}

		public static SyncerDataModifier GetDeleteActionModifier(SyncerData data)
		{
			return new DeleteActionSyncerDataModifier(data);
		}
	}
}
