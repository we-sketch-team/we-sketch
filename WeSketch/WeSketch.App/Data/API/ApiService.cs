using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using WeSketch.App.Data.Shapes;
using Microsoft.AspNet.SignalR.Client;
using System.Net;
using WeSketch.App.Model;
using System.Windows.Threading;
using System.IO;
using System.Windows;
using WeSketch.Common;

namespace WeSketch.App.Data.API
{
    public class ApiService : IAPI
    {
        private string ServerURI = Global.ServerURI;
        private HubConnection connection;
        IHubProxy userHub, boardHub, groupsHub, chatHub;
        IWorkspace workspace;

        public ApiService()
        {
            connection = new HubConnection(ServerURI);
            connection.TraceLevel = TraceLevels.All;
            ServicePointManager.DefaultConnectionLimit = 10;
			HubsSetup();

			try
			{
				connection.Start().Wait();
			}
			catch (Exception)
			{
				System.Windows.Application.Current.Shutdown();
			}
		}

        private void HubsSetup()
        {
            UserHubSetup();
            BoardHubSetup();
            GroupsHubSetup();
            ChatHubSetup();
        }

        private void GroupsHubSetup()
        {
            groupsHub = connection.CreateHubProxy("GroupRegistrationHub");
        }

        private void UserHubSetup()
        {
            userHub = connection.CreateHubProxy("UserHub");
        }

        private void BoardHubSetup()
        {
            boardHub = connection.CreateHubProxy("BoardHub");

            boardHub.On<Board>("NotifyBoardUpdate", (board) => Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
             {
                 workspace.UpdateBoardContent(board);
             })));

            boardHub.On<User>("UserEnteredQueueNotify", (user) => Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                if (workspace == null) return;
                var queue = workspace.GetBoard().UserQueue;
                queue.Enqueue(user);
                workspace.UpdateUserQueue(queue);
            })));

            boardHub.On<int>("UserLeftQueueNotify", (userId) => Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                if (workspace == null) return;
                var queue = workspace.GetBoard().UserQueue;
                queue.RemoveFromQueue(userId);
                workspace.UpdateUserQueue(queue);
            })));
        }

        private void ChatHubSetup()
        {
            chatHub = connection.CreateHubProxy("ChatRoomHub");
            chatHub.On<Message>("ReceiveMessage", (message) => Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                workspace.UpdateMessage(message);
            })));
        }

        public List<Board> GetMyBoards(User user)
        {
            var id = user.Id;
            List<Board> boards = new List<Board>();
            List<Board> full = new List<Board>();
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                boards = boardHub.Invoke<List<Board>>("GetMyBoards", id).Result;
                boards.ForEach(b => full.Add(GetBoardById(b.Id)));
            }));

            return full;
        }

        public Board GetBoardById(int id)
        {
            Board board = new Board();

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                board = boardHub.Invoke<Board>("GetBoardById", id).Result;
            }));

            return board;
        }

        public User Login(string email, string password)
        {
            User user = new User();
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                user = userHub.Invoke<User>("Login", new { Email = email, Password = password }).Result;
                
            }));
            return user;
        }

        public User GetUserByUsername(string username)
        {
            User user = new User();
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                user = userHub.Invoke<User>("GetUserByUsername", username).Result;
            }));

            return user;
        }

        public bool Register(UserRegistrationOptions options)
        {
            User user = new User();
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                user = userHub.Invoke<User>("CreateAccount", options).Result;
            }));

            return user.Username == options.Username;
        }

        public bool CreateBoard(string title, string password, User user)
        {
            Board board = new Board();

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                board = boardHub.Invoke<Board>("CreateBoard", new { Password = password, Title = title, UserId = user.Id, DateCreated = DateTime.Now }).Result;
            }));

            return board.Title == title;
        }

        public bool DeleteBoard(Board board, User user)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                boardHub.Invoke("DeleteBoard", board.Id);
            }));
            return true;
        }

        public void UpdateBoardContent(Board board)
        {
            if (board == null)
                return;
            board.Content = Utilities.ExportShapes(board.Shapes);
            var boardObj = new { Id = board.Id, Content = board.Content };

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                boardHub.Invoke<Board>("UpdateBoardContent", boardObj);
            }));
        }

        public bool AddCollaborator(User user, Board board, string password)
        {
            bool success = false;
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                success = boardHub.Invoke<bool>("JoinBoard", new { UserId = user.Id, BoardId = board.Id, Password = password }).Result;
            }));

            return success;
        }

        public bool RemoveCollaborator(User user, Board board)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                boardHub.Invoke<Board>("RemoveCollaborator", new { UserId = user.Id, BoardId = board.Id });
            }));

            return true;
        }

        public List<User> GetBoardCollaborators(Board board)
        {
            List<User> collabs = null;

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                collabs = boardHub.Invoke<List<User>>("GetCollaborators", board.Id).Result;
            }));

            return collabs;
        }

        public void SubscribeToBoard(Board board)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                groupsHub.Invoke("RegisterToBoardGroup", board.Id);
            }));          
        }

        public void UnsubscribeFromBoard(Board board)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                groupsHub.Invoke("UnsubscribeFromBoardGroup", board.Id);
                //boardHub.Invoke("RemoveCollaborator", new { BoardId = board.Id, UserId = Global.CurrentUser.Id });
            }));
        }

        public void SendMessage(Message message)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                chatHub.Invoke("SendMessage", message);
            }));
        }

        public List<Board> GetSharedBoardsWithUser(User user)
        {
            var id = user.Id;
            List<Board> boards = new List<Board>();

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                boards = boardHub.Invoke<List<Board>>("GetAllBoards", user.Id).Result;
            }));

            return boards;
        }

        public void SetWorkspace(IWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public User GetUserById(int userId)
        {
            User user = new User();
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                user = userHub.Invoke<User>("GetUser", userId).Result;
            }));

            return user;
        }

        public void EnterQueue(User user, Board board)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                var boardUpdater = new { BoardId = board.Id, UserId = user.Id};
                boardHub.Invoke("EnterQueue", boardUpdater);
            }));
        }

        public void LeaveQueue(User user, Board board)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                var boardUpdater = new { BoardId = board.Id, UserId = user.Id };
                boardHub.Invoke("LeaveQueue", boardUpdater);
            }));
        }

        public BoardQueue GetQueue(Board board)
        {
            BoardQueue boardQueue = new BoardQueue();

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                List<User> users = boardHub.Invoke<List<User>>("GetBoardQueue", board.Id).Result;
                users.ForEach(u => boardQueue.Enqueue(u));
            }));

            return boardQueue;
        }
    }
}
