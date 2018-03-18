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
			return false;
        }

        public bool CreateBoard(string title, bool isPublic, User user)
        {
			Board board = new Board() { Title = title, IsPublic = isPublic };
			user.Boards.Add(board);
			return true;
		}

		public bool DeleteBoard(Board board, User user)
        {
			Board boardToDelete = user.Boards.Find(x => x.Id == board.Id);

			if (boardToDelete == null)
				return false;

			user.Boards.Remove(boardToDelete);
			return true;
        }

        public void EnterQueue(User user, Board board)
        {
            throw new NotImplementedException();
        }

        public Board GetBoardById(int id)
        {
			Board board = Global.CurrentUser.Boards.Find(x => x.Id == id);
			return board != null ? board : new Board();
        }

        public List<User> GetBoardCollaborators(Board board)
        {
			return board?.Collaborators.Collaborators;
        }

        public List<Board> GetMyBoards(User user)
        {
            var boards = user.Boards;
            return boards;
        }

        public BoardQueue GetQueue(Board board)
        {
            throw new NotImplementedException();
        }

        public List<Board> GetSharedBoardsWithUser(User user)
        {
			return new List<Board>();
        }

        public List<Board> GetSharedBoardsWithWithUser(User user)
        {
			return new List<Board>();
		}

        public User GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
			return new User();
        }

        public void LeaveQueue(User user, Board board)
        {
            throw new NotImplementedException();
        }

        public User Login(string username, string password)
        {
			return new User();
		}

		public bool Register(UserRegistrationOptions options)
        {
			return false;
        }

        public bool RemoveCollaborator(User user, Board board)
        {
			return false;
        }

        public void SendMessage(Message message)
        {
			return;
        }

        public void SetWorkspace(IWorkspace workspace)
        {
            throw new NotImplementedException();
        }

        public void SubscribeToBoard(Board board)
        {
			return;
        }

        public void UnsubscribeFromBoard(Board board)
        {
			return;
        }

        public void UpdateBoardContent(Board board)
        {
			return;
        }
    }
}
