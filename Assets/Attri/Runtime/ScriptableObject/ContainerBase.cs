using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
    #region Common
    public class Container<T> : ScriptableObject
    {
        public List<T> values = new ();
    }
    public class ListContainer<T> : ScriptableObject
    {
        public List<ListWrapper<T>> elements = new (){new ListWrapper<T>()};
    }
    [Serializable]
    public class ListWrapper<T>
    {
        public List<T> values = new ();
        public T this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }
        public int Count => values.Count;
        public void Add(T value) => values.Add(value);
        public void Remove(T value) => values.Remove(value);
        public void RemoveAt(int index) => values.RemoveAt(index);
        public void Clear() => values.Clear();
        
        public static implicit operator List<T>(ListWrapper<T> wrapper) => wrapper.values;
        public static implicit operator ListWrapper<T>(List<T> list) => new() {values = list};
    }
    #endregion
    
    
    #region Byte
    // [CreateAssetMenu(fileName = nameof(ByteContainer), menuName = "Attri/ScriptableObject/Byte")]
    // public class ByteContainer : Container<byte> { }
    // [CreateAssetMenu(fileName = nameof(ByteListContainer), menuName = "Attri/ScriptableObject/ByteArray")]
    // public class ByteListContainer : ListContainer<byte> { }
    #endregion
}
