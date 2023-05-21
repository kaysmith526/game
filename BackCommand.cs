using System;
using System.Xml.Linq;
using StarterGame.Rooms;

namespace StarterGame.Commands
{
    public class BackCommand : Command
    {

        public BackCommand() : base()
        {
            Name = "back";
            Usage = "back";
        }

        public override bool Execute(Player player)
        {
            player.ReturnToPrevious();
            return false;
        }
    }
}

