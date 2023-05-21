using StarterGame;
using System;

public class Key : IItem
{
    private string _name;
    private float _weight;
    private float _value;
    private string _uuid;

    public string Name { get { return _name; } }
    public float Weight { get { return _weight; } }
    public float Value { get { return _value; } }
    public string Description { get { return Name + ", weight=" + Weight; } }

    public Key() : this("UnknownKey") { }
    public Key(string name) : this(name, 1f) { }
    public Key(string name, float weight) : this(name, weight, 1f) { }

    public Key(string name, float weight, float value)
    {
        _name = name;
        _weight = weight;
        _value = value;
        _uuid = Guid.NewGuid().ToString(); //makes sure each key is unique using uuid
    }

    public bool Equals(Key other)
    {
        return _uuid == other._uuid;
    }
}