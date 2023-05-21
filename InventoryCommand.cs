using System.Collections;
using System.Collections.Generic;

namespace StarterGame.Commands
{
    public class InventoryCommand : Command
    {

        public InventoryCommand() : base()
        {
            Name = "inventory";
            Usage = "inventory";
        }

        override
        public bool Execute(Player player)
        {
            player.OutputMessage("\n" + player.GetItems());
            return false;
        }
    }
}
