using System;

namespace StarterGame.
    
    s
{
    public class Item : IItem
    {
        private string _name;
        private float _weight;
        private float _value;

        public string Name { get { return _name; } }
        public float Weight { get { return _weight; } }
        public float Value { get { return _value; } }
        public string Description { get { return Name + ", weight=" + Weight; } }

        public Item() : this("NoName") { }
        public Item(string name) : this(name, 1f) { }
        public Item(string name, float weight) : this(name, weight, 1f) { }

        public Item(string name, float weight, float value)
        {
            _name = name;
            _weight = weight;
            _value = value;
        }
    }
}

