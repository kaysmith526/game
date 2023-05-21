using System;

namespace StarterGame.Commands
{
    public class SayCommand : Command
    {
        public SayCommand() : base()
        {
            Name = "say";
            Usage = "say <phrase>";
        }

        override
        public bool Execute(Player player)
        {
            bool answer = true;
            if (HasSecondWord())
            {
                player.OutputMessage("\nI cannot say " + SecondWord);
                answer = false;
            }
            return answer;
        }

    }
}

