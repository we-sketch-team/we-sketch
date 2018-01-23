﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Model
{
    public interface IWorkspace
    {
        void AddShape(IShape shape);
        bool AddCollaborator(string username);
        void RemoveCollaborator(User user);
        void SetBoard(Board board);
        Board GetBoard();
        void SaveBoard();
        CollaboratorList LoadBoardCollaborators();
    }
}
