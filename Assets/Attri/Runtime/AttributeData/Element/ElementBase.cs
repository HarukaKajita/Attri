using System;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public abstract class ElementBase : IDataProvider
    {
        // ElementBase
        public int ComponentCount() => Dimension();
        public virtual int[] ComponentsAsInt() => AsInt()[0][0];
        public virtual float[] ComponentsAsFloat() => AsFloat()[0][0];
        public virtual string[] ComponentsAsString() => AsString()[0][0];
        
        // IDataProvider
        public virtual int Dimension()
        {
            return GetAttributeType() switch
            {
                AttributeDataType.Float => ComponentsAsFloat().Length,
                AttributeDataType.Int => ComponentsAsInt().Length,
                AttributeDataType.String => ComponentsAsString().Length,
                _ => throw new System.NotImplementedException()
            };
        }
        public virtual AttributeDataType GetAttributeType() => throw new System.NotImplementedException();
        // [frame][element][component]
        public virtual float[][][] AsFloat() => throw new System.NotImplementedException();
        public virtual int[][][] AsInt() => throw new System.NotImplementedException();
        public virtual string[][][] AsString() => throw new System.NotImplementedException();
        public virtual ScriptableObject GetScriptableObject() => throw new System.NotImplementedException();
    }
}
