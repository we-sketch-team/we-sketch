using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Data.API
{
    public interface IAPI
    {
        User GetUserByUsername(string username);
        User Login(string username, string password);
        bool Register(UserRegistrationOptions options);
        bool CreateBoard(string title, bool isPublic, User user);
        bool DeleteBoard(Board board, User user);
        BoardList GetMyBoards(User user);
        bool AddCollaborator(User user, Board board);
        bool RemoveCollaborator(User user, Board board);
        CollaboratorList GetBoardCollaborators(Board board);
    }
}
