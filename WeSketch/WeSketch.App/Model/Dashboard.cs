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

        public bool CreateBoard(string title, string password)
        {
            // check if board title is unique for user
            var user = Global.CurrentUser;
            var service = SketchService.GetService();
            service.CreateBoard(title, password, user);
            return true;
        }

        public bool DeleteBoard(Board board)
        {
            var user = Global.CurrentUser;
            var service = SketchService.GetService();
            service.DeleteBoard(board, user);
            return true;
        }

        public List<Board> GetOtherBoards()
        {
            var user = Global.CurrentUser;
            var service = SketchService.GetService();
            return service.GetSharedBoardsWithUser(user);
        }

        public List<Board> GetCurrentUserBoardList()
        {
            var user = Global.CurrentUser;
            var service = SketchService.GetService();
            user.Boards = service.GetMyBoards(user);      
            return user.Boards;
        }

        public bool JoinBoard(Board board, string password)
        {
            var service = SketchService.GetService();
            return (service.AddCollaborator(Global.CurrentUser, board, password));
            
        }
    }
}
