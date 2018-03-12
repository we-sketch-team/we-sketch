using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.Common;

namespace WeSketch.App.Model
{
    public interface IBoardContentObserver
    {
        void UpdateBoardContent(Board board);
        void UpdateMessage(Message message);
    }
}
