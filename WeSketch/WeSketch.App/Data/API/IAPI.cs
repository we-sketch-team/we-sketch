using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.API
{
    public interface IAPI
    {
        User Login(string username, string password);
        bool Register(UserRegistrationOptions options);
        bool CreateBoard(string title, bool isPublic, User user);
        BoardList GetMyBoards(User user);
    }
}
