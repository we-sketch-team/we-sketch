using System;
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
    }
}
