using System;

namespace StarterGame.Commands
{
    public class TransportCommand : Command
    {
        public TransportCommand() : base()
        {
            Name = "transport";
            Usage = "transport <room>";
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
                player.OutputMessage("\nTransport To Where?\n" + player.CurrentRoom.GetExits());
            }
            return false;
        }
    }
}
