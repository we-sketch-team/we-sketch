using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Common.RemoteMethods
{
	public static class BoardHubMethods
	{
		public static string GetMyBoards
		{
			get
			{
				return "GetMyBoards";
			}
		}
		public static string CreateBoard
		{
			get
			{
				return "CreateBoard";
			}
		}
		public static string GetBoardWithRole
		{
			get
			{
				return "GetBoardWithRole";
			}
		}
		public static string UpdateBoardContent
		{
			get
			{
				return "UpdateBoardContent";
			}
		}
		public static string UpdateBoard
		{
			get
			{
				return "UpdateBoard";
			}
		}
		public static string DeleteBoard
		{
			get
			{
				return "DeleteBoard";
			}
		}
		public static string GetCollaborators
		{
			get
			{
				return "GetCollaborators";
			}
		}
		public static string AddCollaborator
		{
			get
			{
				return "AddCollaborator";
			}
		}
	}
}
