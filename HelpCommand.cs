using System.Collections;
using System.Collections.Generic;

namespace StarterGame.Commands
{
    public class HelpCommand : Command
    {
        private CommandWords _words;

        public HelpCommand() : this(new CommandWords()) { }

        // Designated Constructor
        public HelpCommand(CommandWords commands) : base()
        {
            _words = commands;
            Name = "help";
            Usage = "help <command>";
        }

        override
        public bool Execute(Player player)
        {
            if (HasSecondWord())
            {
                Command command = _words.Get(SecondWord);
                if (command != null) //does command passed as second word exist?
                {
                    player.OutputMessage("Here's how to use >" + command.Name + "\n* " + command.Usage + "\n");
                }
                else
                {
                    player.OutputMessage("\nThere is no command called >" + SecondWord);
                }
            }
            else
            {
                player.OutputMessage("Here is a list of commands available to you\n" + _words.Description());
            }
            return false;
        }
    }
}
