using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Common.RemoteMethods
{
	public static class UserHubMethods
	{
		public static string Login
		{
			get
			{
				return "Login";
			}
		}
		public static string CreateAccount
		{
			get
			{
				return "CreateAccount";
			}
		}
		public static string GetUser
		{
			get
			{
				return "GetUser";
			}
		}
		public static string UpdateUser
		{
			get
			{
				return "UpdateUser";
			}
		}
		public static string GetUserByUsername
		{
			get
			{
				return "GetUserByUsername";
			}
		}
		public static string DeleteUser
		{
			get
			{
				return "DeleteUser";
			}
		}
	}
}
