﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.DataLayer.Model
{
    public class Board
    {
        public Board()
        {
            UserBoards = new HashSet<UserBoards>();            
        }

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public bool ActiveBoard { get; set; }
        public string Title { get; set; }
        public string Desription { get; set; }
        public string Content { get; set; }
		public string Password { get; set; }

		public virtual ICollection<UserBoards> UserBoards { get; set; }
    }
}
