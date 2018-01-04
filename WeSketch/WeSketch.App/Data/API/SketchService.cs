using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using WeSketch.App.Data.API.DTO;

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

        public bool CreateBoard(string title)
        {
            throw new NotImplementedException();
        }

        public List<Board> GetMyBoards()
        {
            throw new NotImplementedException();
        }

        public bool Login(string email, string password)
        {
            var req = new RestRequest();
            req.AddParameter("Email", email);
            req.AddParameter("Password", password);
            req.Resource = "users/logic/login";
            req.Method = Method.POST;

            UserDetailsDTO res = Execute<UserDetailsDTO>(req);
            return res.Id != -1;
        }

        public bool Register(UserRegistrationOptions options)
        {
            var req = new RestRequest();
            req.AddParameter("Username", options.Username);
            req.AddParameter("Email", options.Email);
            req.AddParameter("Password", options.Password);
            req.Resource = "users/logic/createaccount";
            req.Method = Method.POST;

            CreateUserDTO user = Execute<CreateUserDTO>(req);
            return user.Username == options.Username;
        }
    }
}
