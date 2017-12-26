using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.DataLayer.Model;
using WeSketch.DataLayer.Repository.Repositories;

namespace WeSketch.DataLayer.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        #region  Singleton implementation
        private static UnitOfWork unitOfWorkInstnce;
        private static object sync = new object();

        private UnitOfWork()
        {
        }

        public static UnitOfWork UnitOfWorkInstance
        {
            get
            {
                if(unitOfWorkInstnce == null)
                {
                    lock (sync)
                    {
                        if (unitOfWorkInstnce == null)
                            unitOfWorkInstnce = new UnitOfWork();
                    }
                }

                return unitOfWorkInstnce;
            }
        }
        #endregion
        #region DB context
        private WeSketchContext context = new WeSketchContext();
        #endregion     
        #region Repository variables
        private BoardRepository boardRepository;
        private ChatRoomRepository chatRoomRepository;
        private MessageRepository messageRepository;
        private UserRepository userRepository;
        #endregion
        #region Repository properties
        public BoardRepository BoardRepository
        {
            get
            {
                return this.boardRepository ?? new BoardRepository(context);
            }
        }

        public ChatRoomRepository ChatRoomRepository
        {
            get
            {
                return this.chatRoomRepository ?? new ChatRoomRepository(context);
            }
        }
        
        public MessageRepository MessageRepository
        {
            get
            {
                return this.messageRepository ?? new MessageRepository(context);
            }
        }
        
        public UserRepository UserRepository
        {
            get
            {
                return this.userRepository ?? new UserRepository(context);
            }
        }
        #endregion
        #region Methods
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
