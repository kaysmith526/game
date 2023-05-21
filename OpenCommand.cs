using System;

namespace StarterGame.Commands
{
    public class OpenCommand : Command
    {
        public OpenCommand() : base()
        {
            Name = "open";
            Usage = "open <room>";
        }

        override
        public bool Execute(Player player)
        {
            if (HasSecondWord())
            {
                player.Open(SecondWord);
            }
            else
            {
                player.OutputMessage("\nOpen What Door?\n" + player.CurrentRoom.GetExits());
            }
            return false;
        }
    }
}
