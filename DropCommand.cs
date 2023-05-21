using System.Collections;
using System.Collections.Generic;

namespace StarterGame.Commands
{
    public class DropCommand : Command
    {

        public DropCommand() : base()
        {
            Name = "drop";
            Usage = "drop <item>";
        }

        public override bool Execute(Player player)
        {
            if (HasSecondWord())
            {
                player.Drop(SecondWord);
            }
            else
            {
                player.OutputMessage("\nDrop what?\n" + player.CurrentRoom.GetItems());
            }
            return false;
        }
    }
}
