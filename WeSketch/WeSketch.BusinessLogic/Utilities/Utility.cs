﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.DataLayer.Model;

namespace WeSketch.BusinessLogic.Utilities
{
    public static class Utility
    {
        public static bool IsFavorite(User user, Board board)
        {
            return board.UserBoards.ToList().Find(x => x.User == user).IsFavoriteToUser;
        }

        public static string GetRole(User user, Board board)
        {
			string role = board.UserBoards.ToList().Find(x => x.User == user).Role;
			return role == null || role == string.Empty? "Visitor" : role;
        }

        public static string CreatorRole()
        {
            return "Creator";
        }

        public static string CollaboratorRole()
        {
            return "Collaborator";
        }
		
		public static bool IsPasswordProtected(string password)
		{
			return password != null && password != string.Empty;
		}
    }
}
