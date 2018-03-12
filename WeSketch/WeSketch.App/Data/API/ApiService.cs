﻿using System;
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
        IBoardContentObserver workspace;

        public ApiService()
        {
            connection = new HubConnection(ServerURI);
            //var writer = new StreamWriter("ClientLog.txt");
            //writer.AutoFlush = true;
            connection.TraceLevel = TraceLevels.All;
            //connection.TraceWriter = writer;
            ServicePointManager.DefaultConnectionLimit = 10;
            HubsSetup();
            connection.Start().Wait();
        }

        private void HubsSetup()
        {
            UserHubSetup();
            BoardHubSetup();
            GroupsHubSetup();
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
        }

        private void ChatHubSetup()
        {
            chatHub = connection.CreateHubProxy("ChatRoomHub");
            boardHub.On<Message>("ReceiveMessage", (message) => Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                
            })));

        }

        public List<Board> GetMyBoards(User user)
        {
            var id = user.Id;
            List<Board> boards = new List<Board>();

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                boards = boardHub.Invoke<List<Board>>("GetMyBoards", id).Result;
                boards.ForEach(b => b.Collaborators.Collaborators = GetBoardCollaborators(b));
            }));

            return boards;
        }

        public Board GetBoardById(int id)
        {
            Board board = new Board();

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                board = boardHub.Invoke<Board>("GetBoardWithRole", Global.CurrentUser.Id, id).Result;
                board.Collaborators.Collaborators = GetBoardCollaborators(board);
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

        public bool CreateBoard(string title, bool isPublic, User user)
        {
            Board board = new Board();

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                board = boardHub.Invoke<Board>("CreateBoard", new { PublicBoard = isPublic, Title = title, UserId = user.Id, DateCreated = DateTime.Now }).Result;
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
            board.Content = Utilities.ExportShapes(board.Shapes);
            var boardObj = new { Id = board.Id, Content = board.Content };

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                boardHub.Invoke<Board>("UpdateBoardContent", boardObj);
            }));
        }

        public bool AddCollaborator(User user, Board board)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                boardHub.Invoke<Board>("AddCollaborator", new { UserId = user.Id, BoardId = board.Id });
            }));

            return true;
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

        public void SetBoardContentObserver(IBoardContentObserver observer)
        {
            workspace = observer;
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
            }));
        }
    }
}
