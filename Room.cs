using System.Collections;
using System.Collections.Generic;
using System;
using System.Buffers;
using System.Xml.Linq;

namespace StarterGame.Rooms
{
    public class Room
    {
        private string _name;
        private string _tag;
        private Dictionary<string, Door> _exits;
        private Dictionary<string, IItem> _items;
        private Dictionary<string, IItemContainer> _containers;

        public IRoomDelegate Delegate { get; set; }

        public string Name { get { return _name; } }
        public string Tag { get { return _tag; } }

        public Room(string name) : this(name, "No Tag") { }
        public Room(string name, string tag)
        {
            _exits = new Dictionary<string, Door>();
            _items = new Dictionary<string, IItem>();
            _containers = new Dictionary<string, IItemContainer>();
            _name = name;
            _tag = tag;
        }

        public string Description()
        {
            //combines multiple line with the new line separator
            return string.Join("\n",
                "You are in " + Name,
                Tag,
                "*** " + GetExits());
        }

        public void SetExit(Room room)
        {
            Door door = new Door(this, room);
            door.Open();
            this._exits[room.Name] = door;
            room._exits[this.Name] = door; //applies opposite connection
        }

        //overload to set if door is opened by default
        public void SetExit(Room room, bool isOpened)
        {
            Door door = new Door(this, room);
            if (isOpened) { door.Open(); }
            this._exits[room.Name] = door;
            room._exits[this.Name] = door; //applies opposite connection
        }

        //overload to create locked door with given key
        public void SetExit(Room room, Key key)
        {
            Door door = new Door(this, room, key);
            this._exits[room.Name] = door;
            room._exits[this.Name] = door; //applies opposite connection
        }

        public Door GetExit(string exitName)
        {
            _exits.TryGetValue(exitName, out var door);
            return door;
        }

        public string GetExits()
        {
            string exits = "Room Options: ";
            Dictionary<string, Door>.KeyCollection keys = _exits.Keys;
            foreach (string exitName in keys)
            {
                exits += " " + exitName;
            }

            return exits;
        }

        public string GetItems()
        {
            string items = "Items Available: ";
            Dictionary<string, IItem>.KeyCollection keys = _items.Keys;
            foreach (string itemName in keys)
            {
                items += " " + itemName;
            }

            return items;
        }

        //add to room item list
        public void Drop(IItem item)
        {
            _items.Add(item.Name, item);
        }

        public void Inspect(IItem item)
        {
            _items.Add(item.Name, item);

        }

        public IItem Pick(string name)
        {
            _items.Remove(name, out var item);
            return item;
        }

        public bool ContainsItems(string name)
        {
            return _items.ContainsKey(name);
        }
    }

    public class TrapRoom : IRoomDelegate
    {
        public string Unlocked { get; set; }
        public Room ContainingRoom { get; set; }

        public TrapRoom() : this("password") { }
        public TrapRoom(string unlockWord)
        {
            Unlocked = unlockWord;
            NotificationCenter.Instance.AddObserver("PlayerDidSayword", PlayerDidSayWord);
        }

        public Door GetExit(string exitName)
        {
            return null;
        }

        public string GetExits()
        {
            return "";
        }


        public string Description()
        {
            return "You are in a trap room. GOOD LUCK GETTING OUT HA";
        }

        public void PlayerDidSayWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                if (player.CurrentRoom.Delegate == this)
                {
                    Dictionary<string, object> userInfo = notification.UserInfo;
                    string word = (string)userInfo["word"];
                    if (word.Equals(Unlocked))
                    {
                        player.OutputMessage("That is correct");
                        player.CurrentRoom.Delegate = null;
                        player.OutputMessage("\n" + player.CurrentRoom.Delegate);
                        NotificationCenter.Instance.RemoveObserver("PlayerDidSayWord", PlayerDidSayWord);
                    }
                    else
                    {
                        player.OutputMessage("That's not it:" + word);

                    }
                }
            }
        }
    }

    public class EchoRoom : IRoomDelegate
    {
        public Room ContainingRoom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Door GetExit(string exitName)
        {
            return null;
        }

        public string GetExits()
        {
            return "Echo Room";
        }

        public string Description()
        {
            string description = "You are in an echo room.";
            ContainingRoom.Delegate = null;
            description += ContainingRoom.Description();
            ContainingRoom.Delegate = this;

            return description;
        }
        public void PlayerDidSayWord(Notification notification)
        {
            Player player = (Player)notification.Object;

            if (player != null)
            {
                if (player.CurrentRoom.Delegate == this)
                {
                    Dictionary<string, object> userInfo = notification.UserInfo;
                    string word = (string)userInfo["word"];
                    player.OutputMessage("\n" + word + "..." + word + "..." + word + "\n");
                }
            }
        }
    }
}
