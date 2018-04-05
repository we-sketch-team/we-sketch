using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Server.Queues
{
	public class BoardUpdater
	{
		public string ConnectionId { get; set; }
		public int BoardId { get; set; }
		public int UserId { get; set; }
	}
}
