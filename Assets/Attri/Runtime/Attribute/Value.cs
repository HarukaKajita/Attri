using System;

namespace Attri.Runtime
{
    [Serializable]
    public class Value<T>
    {
        public T[] components = new T[0];

        public Value() : this(new T[] { }) {}
        public Value(T[] value)
        {
            components = value;
        }
        public int Length => components.Length;
        public T this[int index]
        {
            get => components[index];
            set => components[index] = value;
        }
        public override string ToString()
        {
            return $"Value<{typeof(T)}>[{components.Length}]";
        }
    }
}
