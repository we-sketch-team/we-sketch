using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.API;

namespace WeSketch.App.Model
{
    public class Dashboard : IDashboard
    {
        public Dashboard()
        {
            
        }

        public bool CreateBoard(string title, bool isPublic)
        {
            // check if board title is unique for user
            var user = Global.CurrentUser;
            var service = SketchService.GetService();
            service.CreateBoard(title, isPublic, user);
            return true;
        }

        public bool DeleteBoard(Board board)
        {
            // check if user can delete the board
            var user = Global.CurrentUser;
            var service = SketchService.GetService();
            service.DeleteBoard(board, user);
            return true;
        }

        public List<Board> GetCurrentUserBoardList()
        {
            var user = Global.CurrentUser;
            var service = SketchService.GetService();
            user.Boards = service.GetMyBoards(user);      
            return user.Boards;
        }
    }
}
