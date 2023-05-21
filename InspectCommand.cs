using System;

namespace StarterGame.Commands
{
    public class InspectCommand : Command
    {

        public InspectCommand() : base()
        {
            Name = "inspect";
            Usage = "inspect <item>";
        }

        override
        public bool Execute(Player player)
        {
            bool answer = true;
            if (HasSecondWord())
            {
                player.Inspect(SecondWord);
            }
            else
            {
                player.OutputMessage("\nInspect what?\n" + player.CurrentRoom.GetItems());
            }
            return false;
        }
    }
}