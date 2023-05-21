using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame.Commands
{
    public class CommandWords
    {
        private Dictionary<string, Command> commands;
        private static Command[] commandArray =
        {
            new ClockCommand(),
            new EnterCommand(),
            new BackCommand(),
            new OpenCommand(),
            new CloseCommand(),
            new PickCommand(),
            new DropCommand(),
            new InventoryCommand(),
            new SayCommand(),
            new InspectCommand(),
            new QuitCommand(),
            new TransportCommand(),
        };

        public CommandWords() : this(commandArray) { }

        // Designated Constructor
        public CommandWords(Command[] commandList)
        {
            commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                commands[command.Name] = command;
            }
            Command help = new HelpCommand(this);
            commands[help.Name] = help;
        }

        public Command Get(string word)
        {
            Command command = null;
            commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = commands.Keys;
            foreach (string commandName in keys)
            {
                Command command = commands[commandName];
                commandNames += "* " + command.Usage + "\n";
            }
            return commandNames;
        }
    }
}
