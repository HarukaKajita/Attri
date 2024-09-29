using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
    #region Common
    public class Container<T> : ScriptableObject
    {
        [SerializeField, SerializeReference]
        public List<T> values = new ();
    }
    public class ArrayContainer<T> : ScriptableObject
    {
        public List<List<T>> values = new ();
    }
    #endregion
    
    #region String
    [CreateAssetMenu(fileName = nameof(StringContainer), menuName = "Attri/ScriptableObject/String")]
    public class StringContainer : Container<string> { }
    [CreateAssetMenu(fileName = nameof(StringArrayContainer), menuName = "Attri/ScriptableObject/StringArray")]
    public class StringArrayContainer : ArrayContainer<string> { }
    #endregion
    
    #region Byte
    [CreateAssetMenu(fileName = nameof(ByteContainer), menuName = "Attri/ScriptableObject/Byte")]
    public class ByteContainer : Container<byte> { }
    [CreateAssetMenu(fileName = nameof(ByteArrayContainer), menuName = "Attri/ScriptableObject/ByteArray")]
    public class ByteArrayContainer : ArrayContainer<byte> { }
    #endregion
}
