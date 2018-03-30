using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.Common.CommonClasses;

namespace WeSketch.App.Data
{
	public static class Global
	{
		public static User CurrentUser { get; set; }
		public static string ServerURI { get; set; } = "http://localhost:15000/"; // Initial is local server
		public static SyncerData Syncer { get; set; }
}
}
