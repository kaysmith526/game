using System.Collections;
using System.Collections.Generic;

namespace StarterGame.Commands
{
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            Name = "quit";
            Usage = "quit";
        }

        override
        public bool Execute(Player player)
        {
            bool answer = true;
            if (HasSecondWord())
            {
                player.OutputMessage("\nI cannot quit " + SecondWord);
                answer = false;
            }
            return answer;
        }
    }
}
