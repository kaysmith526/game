using System;
using System.Collections.Generic;
using System.Text;
using StarterGame.Rooms;

namespace StarterGame
{
    public interface IWorldEvent
    {
        Room Trigger { get; }

        void Execute();
    }

    public interface IRoomDelegate
    {
        Room ContainingRoom { set; get; }
        Door GetExit(string exitName);
        string GetExits();
        string Description();
    }
}



