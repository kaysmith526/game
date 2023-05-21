using System.Collections;
using System.Collections.Generic;

namespace StarterGame.Commands
{
    public class PickCommand : Command
    {

        public PickCommand() : base()
        {
            Name = "pick";
            Usage = "pick <item>";
        }

        public override bool Execute(Player player)
        {
            if (HasSecondWord())
            {
                player.Pick(SecondWord);
            }
            else
            {
                player.OutputMessage("\nPick what?\n" + player.CurrentRoom.GetItems());
            }
            return false;
        }
    }
}
