using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Model;
using WeSketch.App.View;
using WeSketch.Common;
using WeSketch.Common.CommonClasses;

namespace WeSketch.App.Data.API
{
    public class ProxyService : IAPI, IConnectionObserver
    {
		private ApiService onlineService;
        private bool hasInternetConnection;
        
		public ProxyService()
        {
			onlineService = new ApiService();
            hasInternetConnection = ConnectionNotifier.Instance.HasConnection;
            ConnectionNotifier.Instance.Attach(this);
        }

		public bool AddCollaborator(User user, Board board, string password)
        {
			if (!hasInternetConnection)
				return false;

			return onlineService.AddCollaborator(user, board, password);
        }

        public bool CreateBoard(string title, string password, User user)
        {
			if (hasInternetConnection)
			{
				onlineService.CreateBoard(title,password, user);
				return true;
			}

			Board board = new Board() { Title = title, IsPasswordProtected = string.IsNullOrEmpty(password) };
			user.Boards.Add(board);

			SyncerData data = Global.Syncer;
			SyncerDataModifier modifier = SynhronizerModifierFactory.GetCreateActionModifier(data);
			modifier.Modify(new CommonBoard() { Title = title, Password = password, UserId = user.Id });
			Global.Syncer = modifier.GetModifiedData();

			return true;
		}

		public bool DeleteBoard(Board board, User user)
        {
			if (hasInternetConnection)
			{
				onlineService.DeleteBoard(board, user);
				return true;
			}

			Board boardToDelete = user.Boards.Find(x => x.Id == board.Id);

			if (boardToDelete == null)
				return false;

			user.Boards.Remove(boardToDelete);

			SyncerData data = Global.Syncer;
			SyncerDataModifier modifier = SynhronizerModifierFactory.GetDeleteActionModifier(data);
			modifier.Modify(new CommonBoard() { Title = board.Title, Content = board.Content, UserId = user.Id });
			Global.Syncer = modifier.GetModifiedData();
			return true;
        }

        public void EnterQueue(User user, Board board)
        {
			if(!hasInternetConnection)
				return;

			onlineService.EnterQueue(user, board);
        }

        public Board GetBoardById(int id)
        {
			if (hasInternetConnection)
				return onlineService.GetBoardById(id);

			Board board = Global.CurrentUser.Boards.Find(x => x.Id == id);
			return board != null ? board : new Board();
        }

        public List<User> GetBoardCollaborators(Board board)
        {
			if (hasInternetConnection)
				return onlineService.GetBoardCollaborators(board);

			return board?.Collaborators.Collaborators;
        }

        public List<Board> GetMyBoards(User user)
        {
			if (hasInternetConnection)
				return onlineService.GetMyBoards(user);

			var boards = user.Boards;
            return boards;
        }

        public BoardQueue GetQueue(Board board)
        {
			if (hasInternetConnection)
				return onlineService.GetQueue(board);

			return new BoardQueue();
        }

        public List<Board> GetSharedBoardsWithUser(User user)
        {
			return hasInternetConnection ? onlineService.GetSharedBoardsWithUser(user) : new List<Board>();
        }

        public List<Board> GetSharedBoardsWithWithUser(User user)
        {
			 return new List<Board>();
		}

        public User GetUserById(int userId)
        {
			return hasInternetConnection ? onlineService.GetUserById(userId) : new User();
        }

        public User GetUserByUsername(string username)
        {
			return hasInternetConnection ? onlineService.GetUserByUsername(username) : new User();
		}

		public void LeaveQueue(User user, Board board)
        {
			if (!hasInternetConnection)
				return;

			onlineService.LeaveQueue(user, board);
        }

        public User Login(string email, string password)
        {
			return hasInternetConnection ? onlineService.Login(email, password) : new User();
		}

		public bool Register(UserRegistrationOptions options)
        {
			if (!hasInternetConnection)
				return false;

			onlineService.Register(options);
			return true;
        }

        public bool RemoveCollaborator(User user, Board board)
        {
			if (!hasInternetConnection)
				return false;

			onlineService.RemoveCollaborator(user, board);
			return true;
		}

        public void SendMessage(Message message)
        {
			if (!hasInternetConnection)
				return;

			onlineService.SendMessage(message);
		}

        public void SetWorkspace(IWorkspace workspace)
        {
			if (!hasInternetConnection)
				return;

			onlineService.SetWorkspace(workspace);
		}

        public void SubscribeToBoard(Board board)
        {
			if (!hasInternetConnection)
				return;

			onlineService.SubscribeToBoard(board);
		}

        public void UnsubscribeFromBoard(Board board)
        {
			if (!hasInternetConnection)
				return;

			onlineService.UnsubscribeFromBoard(board);
		}

        public void UpdateBoardContent(Board board)
        {
			if(hasInternetConnection)
			{
				onlineService.UpdateBoardContent(board);
				return;
			}

            CommonBoard boardToUpdate = new CommonBoard()
            {
                BoardId = board.Id,
				Content = Utilities.ExportShapes(board.Shapes),
				Title = board.Title,
				UserId = Global.CurrentUser.Id
			};

			SyncerData data = Global.Syncer;
			SyncerDataModifier modifier = SynhronizerModifierFactory.GetUpdateActionModifier(data);
			modifier.Modify(new CommonBoard() { Title = board.Title, Content = board.Content, UserId = Global.CurrentUser.Id });
			Global.Syncer = modifier.GetModifiedData();
			return;
        }

        public void UpdateConnectionStatus(bool hasConnection)
        {
            this.hasInternetConnection = hasConnection;
        }
    }
}
