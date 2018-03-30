using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;
using WeSketch.App.Data;

namespace WeSketch.App.Model
{
    public interface IDashboard
    {
        bool CreateBoard(string title, string password);
        bool DeleteBoard(Board board);
        List<Board> GetCurrentUserBoardList();
        List<Board> GetOtherBoards();
    }
}
