using System;

namespace StarterGame.Rooms
{
    public class Door : ICloseable, ILockable
    {
        private Room _roomA;
        private Room _roomB;
        private bool _isOpen;
        private bool _isLocked;
        private Key _key;

        public bool IsOpen { get { return _isOpen; } }
        public bool IsClosed { get { return !_isOpen; } }
        public bool IsLocked { get { return _isLocked; } }
        public bool IsUnlocked { get { return !_isLocked; } }
        public Key Key { get { return _key; } }

        public string KeyName { get { return _key.Name; } }

        //defines an not lockable door
        public Door(Room roomA, Room roomB)
        {
            _roomA = roomA;
            _roomB = roomB;
        }

        //defines a lockable door
        public Door(Room roomA, Room roomB, Key key)
        {
            _roomA = roomA;
            _roomB = roomB;
            _key = key;
        }

        //checks room of opposite side of given room
        public Room RoomOnTheOtherSideOf(Room room)
        {
            Room theOtherRoom = null;
            if (room == _roomA)
            {
                theOtherRoom = _roomB;
            }
            if (room == _roomB)
            {
                theOtherRoom = _roomA;
            }
            return theOtherRoom;
        }

        //opens door
        public bool Open()
        {
            if (!_isLocked)
            {
                _isOpen = true;
                return true;
            }
            return false;
        }

        //closes door
        public bool Close()
        {
            _isOpen = false;
            return true;
        }

        //door can only be locked if it RequiresKey()
        public bool Lock(Key key)
        {
            if (RequiresKey() && _key.Equals(key))
            {
                _isLocked = true;
                return true;
            }
            return false;
        }

        //door can only be locked if it RequiresKey()
        public bool Unlock(Key key)
        {
            if (RequiresKey() && _key.Equals(key))
            {
                _isLocked = false;
                return true;
            }
            return false;
        }

        //if key variable is not null, then requires key
        public bool RequiresKey()
        {
            return _key != null;
        }

        public bool MatchesKey(IItem item)
        {
            if (!(item is Key))
            {
                return false;
            }
            return Key.Equals(item as Key);
        }
    }
}

