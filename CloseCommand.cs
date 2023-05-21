using System.Collections;
using System.Collections.Generic;

namespace StarterGame.Commands
{
    public class CloseCommand : Command
    {

        public CloseCommand() : base()
        {
            Name = "close";
            Usage = "close <room>";
        }

        override
        public bool Execute(Player player)
        {
            if (HasSecondWord())
            {
                player.Close(SecondWord);
            }
            else
            {
                player.OutputMessage("\nClose What Door?\n" + player.CurrentRoom.GetExits());
            }
            return false;
        }
    }
}
