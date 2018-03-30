using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.Common;

namespace WeSketch.App.Data.API
{
    public interface IAPI
    {
        User GetUserByUsername(string username);
        User Login(string username, string password);
        bool Register(UserRegistrationOptions options);
        bool CreateBoard(string title, string password, User user);
        bool DeleteBoard(Board board, User user);
        void UpdateBoardContent(Board board);
        List<Board> GetMyBoards(User user);
        bool AddCollaborator(User user, Board board, string password);
        bool RemoveCollaborator(User user, Board board);
        List<User> GetBoardCollaborators(Board board);
        Board GetBoardById(int id);
        void SetWorkspace(IWorkspace workspace);
        void SubscribeToBoard(Board board);
        void UnsubscribeFromBoard(Board board);
        void SendMessage(Message message);
        List<Board> GetSharedBoardsWithUser(User user);
        User GetUserById(int userId);
        void EnterQueue(User user, Board board);
        void LeaveQueue(User user, Board board);
        BoardQueue GetQueue(Board board);
    }
}
