using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Model;
using WeSketch.Common;

namespace WeSketch.App.Data.API
{
    public class ProxyService : IAPI
    {
        public ProxyService()
        {

        }

        public bool AddCollaborator(User user, Board board)
        {
            throw new NotImplementedException();
        }

        public bool CreateBoard(string title, bool isPublic, User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBoard(Board board, User user)
        {
            throw new NotImplementedException();
        }

        public Board GetBoardById(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetBoardCollaborators(Board board)
        {
            throw new NotImplementedException();
        }

        public List<Board> GetMyBoards(User user)
        {
            var boards = user.Boards;
            return boards;
        }

        public List<Board> GetSharedBoardsWithUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<Board> GetSharedBoardsWithWithUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Register(UserRegistrationOptions options)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCollaborator(User user, Board board)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void SetBoardContentObserver(IBoardContentObserver observer)
        {
            throw new NotImplementedException();
        }

        public void SubscribeToBoard(Board board)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeFromBoard(Board board)
        {
            throw new NotImplementedException();
        }

        public void UpdateBoardContent(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
