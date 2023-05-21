using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame.Commands
{
    public abstract class Command
    {
        private string _name;
        private string _secondWord;
        private string _usage;
        public string Name { get { return _name; } set { _name = value; } }
        public string SecondWord { get { return _secondWord; } set { _secondWord = value; } }
        public string Usage { get { return _usage; } set { _usage = value; } }

        public string ThirdWord { get; internal set; }

        public Command()
        {
            Name = "";
            Usage = "";
            SecondWord = null;
        }

        public bool HasSecondWord()
        {
            return SecondWord != null;
        }

        public abstract bool Execute(Player player);
    }
}
