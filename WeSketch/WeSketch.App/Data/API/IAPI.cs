using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.API
{
    public interface IAPI
    {
        bool Login(string username, string password);
        bool Register(UserRegistrationOptions options);
        bool CreateBoard(string title);
        List<Board> GetMyBoards();
    }
}
