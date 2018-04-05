using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Common.CommonClasses
{
	public class CommonBoard
	{
		public int BoardId { get; set; }
		public int UserId { get; set; }
		public string Content { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public string Password { get; set; }

		public CommonBoard()
		{
		}
	}
}
