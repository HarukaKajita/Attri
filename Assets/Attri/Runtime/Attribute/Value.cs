using System;
using MessagePack;

namespace Attri.Runtime
{
    [Union(0, typeof(Value<int>))]
    [Union(1, typeof(Value<float>))]
    [Union(2, typeof(Value<string>))]
    [MessagePackObject(true)]
    [Serializable]
    public class Value<T>
    {
        public T[] components = new T[0];

        public Value() : this(new T[] { }) {}
        public Value(T[] value)
        {
            components = value;
        }
        [IgnoreMember]
        public int Length => components.Length;
        [IgnoreMember]
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
