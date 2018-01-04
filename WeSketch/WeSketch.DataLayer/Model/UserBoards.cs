using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.DataLayer.Model
{
    public class UserBoards
    {
        public int UserId { get; set; }
        public int BoardId { get; set; }
        public string Role { get; set; }
        public bool IsFavoriteToUser { get; set; }

        public virtual User User { get; set; }
        public virtual Board Board { get; set; }
    }
}
