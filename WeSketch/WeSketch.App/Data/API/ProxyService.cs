using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Model;
using WeSketch.Common;
using WeSketch.Common.CommonClasses;

namespace WeSketch.App.Data.API
{
    public class ProxyService : IAPI
    {
		private ApiService onlineService;
        public bool HasInternetConnection { get; set; } = true;

		public ProxyService()
        {
			onlineService = new ApiService();
			NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
        }

		private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
		{
			HasInternetConnection = e.IsAvailable;
		}

		public bool AddCollaborator(User user, Board board, string password)
        {
			if (!HasInternetConnection)
				return false;

			return onlineService.AddCollaborator(user, board, password);
        }

        public bool CreateBoard(string title, string password, User user)
        {
			if (HasInternetConnection)
			{
				onlineService.CreateBoard(title,password, user);
				return true;
			}

			Board board = new Board() { Title = title, IsPasswordProtected = string.IsNullOrEmpty(password) };
			user.Boards.Add(board);
			Global.Syncer.BoardsToCreate.Add(new CommonBoard() { Title = title, UserId = user.Id, Password = password });
			return true;
		}

		public bool DeleteBoard(Board board, User user)
        {
			if (HasInternetConnection)
			{
				onlineService.DeleteBoard(board, user);
				return true;
			}

			Board boardToDelete = user.Boards.Find(x => x.Id == board.Id);

			if (boardToDelete == null)
				return false;

			user.Boards.Remove(boardToDelete);
			Global.Syncer.BoardsToDelete.Add(board.Id);
			return true;
        }

        public void EnterQueue(User user, Board board)
        {
			if(!HasInternetConnection)
				return;

			onlineService.EnterQueue(user, board);
        }

        public Board GetBoardById(int id)
        {
			if (HasInternetConnection)
				return onlineService.GetBoardById(id);

			Board board = Global.CurrentUser.Boards.Find(x => x.Id == id);
			return board != null ? board : new Board();
        }

        public List<User> GetBoardCollaborators(Board board)
        {
			if (HasInternetConnection)
				return onlineService.GetBoardCollaborators(board);

			return board?.Collaborators.Collaborators;
        }

        public List<Board> GetMyBoards(User user)
        {
			if (HasInternetConnection)
				return onlineService.GetMyBoards(user);

			var boards = user.Boards;
            return boards;
        }

        public BoardQueue GetQueue(Board board)
        {
			if (HasInternetConnection)
				onlineService.GetQueue(board);

			return new BoardQueue();
        }

        public List<Board> GetSharedBoardsWithUser(User user)
        {
			return HasInternetConnection ? onlineService.GetSharedBoardsWithUser(user) : new List<Board>();
        }

        public List<Board> GetSharedBoardsWithWithUser(User user)
        {
			 return new List<Board>();
		}

        public User GetUserById(int userId)
        {
			return HasInternetConnection ? onlineService.GetUserById(userId) : new User();
        }

        public User GetUserByUsername(string username)
        {
			return HasInternetConnection ? onlineService.GetUserByUsername(username) : new User();
		}

		public void LeaveQueue(User user, Board board)
        {
			if (!HasInternetConnection)
				return;

			onlineService.LeaveQueue(user, board);
        }

        public User Login(string email, string password)
        {
			return HasInternetConnection ? onlineService.Login(email, password) : new User();
		}

		public bool Register(UserRegistrationOptions options)
        {
			if (!HasInternetConnection)
				return false;

			onlineService.Register(options);
			return true;
        }

        public bool RemoveCollaborator(User user, Board board)
        {
			if (!HasInternetConnection)
				return false;

			onlineService.RemoveCollaborator(user, board);
			return true;
		}

        public void SendMessage(Message message)
        {
			if (!HasInternetConnection)
				return;

			onlineService.SendMessage(message);
		}

        public void SetWorkspace(IWorkspace workspace)
        {
			if (!HasInternetConnection)
				return;

			onlineService.SetWorkspace(workspace);
		}

        public void SubscribeToBoard(Board board)
        {
			if (!HasInternetConnection)
				return;

			onlineService.SubscribeToBoard(board);
		}

        public void UnsubscribeFromBoard(Board board)
        {
			if (!HasInternetConnection)
				return;

			onlineService.UnsubscribeFromBoard(board);
		}

        public void UpdateBoardContent(Board board)
        {
			if(HasInternetConnection)
			{
				onlineService.UpdateBoardContent(board);
				return;
			}

			CommonBoard boardToUpdate = new CommonBoard()
			{
				Content = board.Content,
				Title = board.Title,
				UserId = Global.CurrentUser.Id
			};

			Global.Syncer.BoardsToUpdate.Add(boardToUpdate);
			return;
        }
    }
}
