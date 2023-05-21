namespace StarterGame
{
    public interface ICloseable
    {
        bool IsClosed { get; }
        bool IsOpen { get; }
        bool Open();
        bool Close();
    }

    public interface ILockable
    {
        bool IsLocked { get; }
        bool IsUnlocked { get; }
        bool Lock(Key key);
        bool Unlock(Key key);
    }

    //public interface IPickable
    //{
    //    bool IsPicked { get; }
    //    bool IsDropped { get; }
    //    bool Pick();
    //    bool Drop();
    //}

    public interface IItem
    {
        string Name { get; }
        float Weight { get; }
        float Value { get; }
        string Description { get; }
    }

    public interface IItemContainer
    {

    }

    public interface IInventory
    {
        
    }
}

