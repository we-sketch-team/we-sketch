using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.API
{
    public class Proxy : IAPI
    {
        private IAPI api;
        public bool HasInternet { get; set; }

        public Proxy()
        {
            api = new SketchService();
            HasInternet = true;
        }

        private bool HasInternetConnection()
        {
            return HasInternet;
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

        public CollaboratorList GetBoardCollaborators(Board board)
        {
            throw new NotImplementedException();
        }

        public BoardList GetMyBoards(User user)
        {
            BoardList boardList;

            if (HasInternetConnection())
                boardList = api.GetMyBoards(user);
            else
                boardList = user.Boards;

            return boardList;
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

        public void UpdateBoardContent(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
