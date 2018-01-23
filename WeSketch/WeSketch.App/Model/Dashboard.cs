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
        private IAPI api;

        public Dashboard()
        {
            api = new SketchService();
        }

        public bool CreateBoard(string title, bool isPublic)
        {
            // check if board title is unique for user
            var user = Global.CurrentUser;
            api.CreateBoard(title, isPublic, user);
            return true;
        }

        public bool DeleteBoard(Board board)
        {
            // check if user can delete the board
            var user = Global.CurrentUser;
            api.DeleteBoard(board, user);
            return true;
        }

        public BoardList GetCurrentUserBoardList()
        {
            var user = Global.CurrentUser;
            var boards = api.GetMyBoards(user);
            user.Boards = boards;
            return user.Boards;
        }
    }
}
