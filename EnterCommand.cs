using System.Collections;
using System.Collections.Generic;

namespace StarterGame.Commands
{
    public class EnterCommand : Command
    {

        public EnterCommand() : base()
        {
            Name = "enter";
            Usage = "enter <room>";
        }

        override
        public bool Execute(Player player)
        {
            if (HasSecondWord())
            {
                player.WaltTo(SecondWord);
            }
            else
            {
                player.OutputMessage("\nEnter Where?\n" + player.CurrentRoom.GetExits());
            }
            return false;
        }
    }
}
