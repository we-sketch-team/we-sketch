using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Data.API
{
    public class SketchService : IAPI
    {
        private const string BaseUrl = "http://localhost:55000/api";

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
            return response.Data;
        }

        public void Execute(RestRequest request)
        {
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);
            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
        }

        public bool CreateBoard(string title, bool isPublic, User user)
        {
            var req = new RestRequest();
            req.AddParameter("Title", title);
            req.AddParameter("PublicBoard", isPublic);
            req.AddParameter("UserId", user.Id);
            req.Resource = "boards/logic/create";
            req.Method = Method.POST;

            Board res = Execute<Board>(req);
            return res.Id != -1;
        }

        public BoardList GetMyBoards(User user)
        {
            var req = new RestRequest();
            req.Resource = $"boards/logic/get/alluserboards/{user.Id}";
            req.Method = Method.GET;
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);
            var result = client.Execute(req);
            var content = result.Content;

            var boards = JsonConvert.DeserializeObject<BoardList>(content);
            return boards;
        }

        public User Login(string email, string password)
        {
            var req = new RestRequest();
            req.AddParameter("Email", email);
            req.AddParameter("Password", password);
            req.Resource = "users/logic/login";
            req.Method = Method.POST;

            User res = Execute<User>(req);
            return res;
        }

        public bool Register(UserRegistrationOptions options)
        {
            var req = new RestRequest();
            req.AddParameter("Username", options.Username);
            req.AddParameter("Email", options.Email);
            req.AddParameter("Password", options.Password);
            req.Resource = "users/logic/createaccount";
            req.Method = Method.POST;

            User user = Execute<User>(req);
            return user.Username == options.Username;
        }

        public bool DeleteBoard(Board board, User user)
        {
            var req = new RestRequest();
            req.Resource = $"boards/logic/delete/{board.Id}";
            req.Method = Method.PUT;
            Execute(req);
            return true;
        }
    }
}
