using System.Collections;
using System.Collections.Generic;
using System;
using StarterGame.Rooms;
using System.Numerics;
using StarterGame.Items;
using System.Data.SqlTypes;

namespace StarterGame
{
    public class Player
    {
        private Room _currentRoom = null;
        private List<Room> _previousRooms = new List<Room>();

        public Room CurrentRoom { get { return _currentRoom; }}
        public List<Room> PreviousRooms { get { return _previousRooms; } }

        private Dictionary<string, IItem> _invertory;

        public Player(Room room)
        {
            _currentRoom = room;
            _invertory = new Dictionary<string, IItem>();
        }

        public void WaltTo(string direction) //enter command
        {
            Door nextDoor = CurrentRoom.GetExit(direction);
            if (nextDoor != null)
            {
                if (nextDoor.IsOpen)
                {
                    _previousRooms.Add(_currentRoom); //current room is no longer current
                    _currentRoom = nextDoor.RoomOnTheOtherSideOf(CurrentRoom);
                    OutputMessage("\n" + CurrentRoom.Description());
                    return;
                }
                if (nextDoor.IsLocked)
                {
                    OutputMessage("\nCannot enter " + direction + ", the door is locked and requires a key called [" + nextDoor.KeyName + "]");
                    return;
                }
                else
                {
                    OutputMessage("\nCannot enter " + direction + ", the door is closed!");
                    return;
                }
            }
            OutputMessage("\nThere is no door on " + direction);
            return;
        }

        public void ReturnToPrevious() //back command  
        {
            if (PreviousRooms.Count > 0)
            {
                _currentRoom = _previousRooms[_previousRooms.Count - 1]; //previous room becomes current room
                _previousRooms.RemoveAt(_previousRooms.Count - 1); //last room is removed from list of previous rooms
                OutputMessage("\nYou returned to " + CurrentRoom.Name + "!");
                OutputMessage("\n" + CurrentRoom.Description());
                return;
            }
            OutputMessage("\nYou cannot go back anymore. You finished retracing back your steps!");
            return;
        }


        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Open(string doorName)
        {
            Door door = CurrentRoom.GetExit(doorName);
            if (door.IsOpen)
            {
                OutputMessage("\nThe door on " + doorName + " is already open");
                return;
            }

            if (door.RequiresKey() && door.IsLocked) //door requires key and door is locked
            {
                if (!InventoryContainsKey(door, out Key key)) //player does not have key
                {
                    OutputMessage("\nThe door must be unlocked with " + door.KeyName);
                    return;
                }

                door.Unlock(key); //unlocks door with key
            }

            if (!door.Open()) //attempting to open door, but fails
            {
                OutputMessage("\nThe door cannot be opened");
                return;
            }

            OutputMessage("\nThe door on " + doorName + " is now open");
        }

        public void Close(string doorName)
        {
            Door door = CurrentRoom.GetExit(doorName);
            if (door.IsClosed) //door is already closed
            {
                OutputMessage("\nThe door on " + doorName + " is already closed");
                return;
            }

            if (!door.Close()) //door attempted to close, but failed
            {
                OutputMessage("\nThe door on" + doorName + " cannot be closed");
                return;
            }

            OutputMessage("\nThe door on " + doorName + " is now closed");
        }

        public void Pick(string name )
        {
            if (CurrentRoom.ContainsItems(name))
            {
                IItem item = CurrentRoom.Pick(name);
                _invertory.Add(name, item);
                OutputMessage("\nYou have picked [" + name + "] from " + CurrentRoom.Name + "!");

                return;
            }
            OutputMessage("\nThere is no item called [" + name + "]!");
            return;
        }

        public void Drop(string name)
        {
            if (_invertory.TryGetValue(name, out var item))
            {
                _invertory.Remove(name);
                CurrentRoom.Drop(item);
                OutputMessage("\nYou have dropped [" + name + "] in " + CurrentRoom.Name + "!");
                return;
            }
            OutputMessage("\nYou have no item with [" + name + "]!");
            return;
        }


        public void Inspect(string name)
        {
            if (_invertory.TryGetValue(name, out var item))
            {
                
                OutputMessage("\n" + item.Description);
                return;
            }
            OutputMessage("\nYou have no item with " + name + "!");
            return;
        }
        public string GetItems()
        {
            string exitNames = "Items in inventory: ";
            Dictionary<string, IItem>.KeyCollection keys = _invertory.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }

            return exitNames;

        }

        public bool InventoryContainsKey(Door door, out Key key)
        {
            Dictionary<string, IItem>.KeyCollection itemNames = _invertory.Keys;
            foreach (string names in itemNames)
            {
                IItem item = _invertory[names];
                if (door.MatchesKey(item))
                {
                    key = item as Key;
                    return true;
                }
            }
            key = null;
            return false;
        }

        //public void Give(IItem item)
        //{
        //    _invertory.Insert(item);

        //}

        //public IItem Take(string itemName)
        //{
        //    return _invertory.Remove(itemName);
        //}
    }


}

