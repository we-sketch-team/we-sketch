using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.Services;
using WeSketch.BusinessLogic.Providers;
using WeSketch.DataLayer.UnitOfWork;

namespace WeSketch.BusinessLogic.Utilities
{
    public static class ObjectFactory
    {
        public static Mediator GetMediator()
        {
            return new Mediator();
        }

        public static DataService GetDataService()
        {
            return new DataService();
        }

        public static BoardProvider GetBoardProvider(Mediator mediator)
        {
            return new BoardProvider(mediator);
        }

        public static ChatRoomProvider GetChatRoomProvider(Mediator mediator)
        {
            return new ChatRoomProvider(mediator);
        }

        public static UserProvider GetUserProvider(Mediator mediator)
        {
            return new UserProvider(mediator);
        }

        public static UnitOfWork GetUnitOfWork()
        {
            return UnitOfWork.UnitOfWorkInstance;
        }
    }
}
